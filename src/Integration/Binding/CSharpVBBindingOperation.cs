﻿/*
 * SonarLint for Visual Studio
 * Copyright (C) 2016-2020 SonarSource SA
 * mailto:info AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading;
using EnvDTE;
using SonarLint.VisualStudio.Core.Helpers;
using Language = SonarLint.VisualStudio.Core.Language;

namespace SonarLint.VisualStudio.Integration.Binding
{
    // Legacy connected mode:
    // * make binding changes to a single project i.e. writes the ruleset files
    // and updates the project file

    internal partial class CSharpVBBindingOperation : ICSharpVBBindingOperation
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ICSharpVBBindingConfig cSharpVBBindingConfig;
        private readonly IFileSystem fileSystem;
        private readonly ISourceControlledFileSystem sourceControlledFileSystem;

        private readonly Dictionary<Property, PropertyInformation> propertyInformationMap = new Dictionary<Property, PropertyInformation>();
        private readonly Project initializedProject;

        public CSharpVBBindingOperation(IServiceProvider serviceProvider, Project project, ICSharpVBBindingConfig cSharpVBBindingConfig)
            : this(serviceProvider, project, cSharpVBBindingConfig, new FileSystem())
        {
        }

        internal CSharpVBBindingOperation(IServiceProvider serviceProvider, Project project, ICSharpVBBindingConfig cSharpVBBindingConfig, IFileSystem fileSystem)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.initializedProject = project ?? throw new ArgumentNullException(nameof(project));
            this.cSharpVBBindingConfig = cSharpVBBindingConfig ?? throw new ArgumentNullException(nameof(cSharpVBBindingConfig));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

            this.sourceControlledFileSystem = this.serviceProvider.GetService<ISourceControlledFileSystem>();
            this.sourceControlledFileSystem.AssertLocalServiceIsNotNull();

            this.ruleSetSerializer = this.serviceProvider.GetService<IRuleSetSerializer>();
            this.ruleSetSerializer.AssertLocalServiceIsNotNull();
        }

        #region State
        internal /*for testing purposes*/ Language ProjectLanguage { get; private set; }

        internal /*for testing purposes*/ string ProjectFullPath { get; private set; }
        internal /*for testing purposes*/ IReadOnlyDictionary<Property, PropertyInformation> PropertyInformationMap { get { return this.propertyInformationMap; } }
        #endregion

        public void Initialize()
        {
            this.CaptureProject();
            this.CalculateRuleSetInformation();
        }

        public void Prepare(CancellationToken token)
        {
            // We want to limit the number of rulesets so for this we use the previously calculated TargetRuleSetFileName
            // and group by it. This handles the special case of all the properties having the same ruleset and also the case
            // in which the user didn't configure anything and we're getting only default value from the properties.
            foreach (IGrouping<string, PropertyInformation> group in this.propertyInformationMap.Values.GroupBy(info => info.TargetRuleSetFileName))
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                string targetRuleSetFileName = group.Key;
                string currentRuleSetFilePath = group.First().CurrentRuleSetFilePath;
                Debug.Assert(group.All(_ => StringComparer.OrdinalIgnoreCase.Equals(currentRuleSetFilePath, currentRuleSetFilePath)), "Expected all the rulesets to be the same when the target rule set name is the same");
                string newRuleSetFilePath = this.QueueWriteProjectLevelRuleSet(this.ProjectFullPath, targetRuleSetFileName, cSharpVBBindingConfig, currentRuleSetFilePath);

                foreach (PropertyInformation info in group)
                {
                    info.NewRuleSetFilePath = newRuleSetFilePath;
                }
            }
        }

        public void Commit()
        {
            AddRuleset();
            EnsureAdditionalFileIsReferenced();
        }

        private void AddRuleset()
        {
            foreach (var keyValue in this.propertyInformationMap)
            {
                Property property = keyValue.Key;
                string ruleSetFullFilePath = keyValue.Value.NewRuleSetFilePath;

                Debug.Assert(!string.IsNullOrWhiteSpace(ruleSetFullFilePath), "Prepare was not called");
                Debug.Assert(fileSystem.File.Exists(ruleSetFullFilePath), "File not written: " + ruleSetFullFilePath);

                string updatedRuleSetValue = PathHelper.CalculateRelativePath(this.ProjectFullPath, ruleSetFullFilePath);
                property.Value = updatedRuleSetValue;

                this.AddFileToProject(this.initializedProject, ruleSetFullFilePath);
            }
        }

        private void EnsureAdditionalFileIsReferenced()
        {
            var projectSystem = serviceProvider.GetService<IProjectSystemHelper>();
            projectSystem.AssertLocalServiceIsNotNull();

            // We assume that we've checked during the "Prepare" step that there are references to other
            // SonarLint.xml files as additional files in the project, and failed the binding if there are.
            // In theory, that leaves three options:
            // * the file isn't referenced -> add it
            // * the file is referenced but with the wrong ItemType -> try to fix it.
            // * the file is correctly referenced -> do nothing
            //
            // In practice, things are more complicated.
            // Scenario:
            // A) Old-style project, file is ref'd in a Directory.Build.props file.
            //      It will be used by MSBuild in command line builds.
            //      However, the VS APIs won't detect the file so we can't tell it exists -> we think the project is not fully bound.
            //      We'll still try to add it, and will succeed -> the file will also be ref'd in each project file.
            //      Summary: no error, but duplicate references in the project files.
            //
            // B) Old-style project, file is *not* ref'd, but SonarLint.xml file exists on disc in the same folder as the project file.
            //      We detect the project is not fully bound -> try to add the file.
            //      This will error with:
            //          "Cannot add a link to the file SonarLint.xml. There is already a file of the same name in this folder.".
            //      This should be a real corner case.
            // 
            // C) New-style project, file is ref'd in a Directory.Build.props file but with the wrong ItemType
            //      We detect the project is not fully bound -> try to change the item type.
            //      This errors with:
            //          "Only included item inside the project can be moved.
            //           Parameter name: currentItem"
            //      Note: this only happens if the ItemType is one that VS knows e.g. "Content". If it is a type
            //      VS does not recognise (e.g. "Foo") then adding the file succeeds.

            var additionalFilePath = cSharpVBBindingConfig.AdditionalFile.Path;
            var refdFile = projectSystem.FindFileInProject(initializedProject, additionalFilePath);
            if (refdFile == null)
            {
                projectSystem.AddFileToProject(initializedProject, additionalFilePath, Constants.AdditionalFilesItemTypeName);
            }
            else
            {
                var property = VsShellUtils.FindProperty(refdFile.Properties, Constants.ItemTypePropertyKey);
                if (!Constants.AdditionalFilesItemTypeName.Equals(property.Value?.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    // This will fail if the existing item ref isn't in the main project file e.g. if it is in a Directory.Build.props file.
                    // We can't do much about this, other than catch and log the error higher up.
                    property.Value = Constants.AdditionalFilesItemTypeName;
                }
            }
        }

        #region Helpers

        private void CaptureProject()
        {
            this.ProjectLanguage = ProjectToLanguageMapper.GetLanguageForProject(this.initializedProject);
            this.ProjectFullPath = this.initializedProject.FullName;
        }

        private void CalculateRuleSetInformation()
        {
            var solutionRuleSetProvider = this.serviceProvider.GetService<ISolutionRuleSetsInformationProvider>();
            RuleSetDeclaration[] ruleSetsInfo = solutionRuleSetProvider.GetProjectRuleSetsDeclarations(this.initializedProject).ToArray();

            string sameRuleSetCandidate = ruleSetsInfo.FirstOrDefault()?.RuleSetPath;

            // Special case: if all the values are the same use project name as the target ruleset name
            bool useSameTargetName = false;
            if (ruleSetsInfo.All(r => StringComparer.OrdinalIgnoreCase.Equals(sameRuleSetCandidate, r.RuleSetPath)))
            {
                useSameTargetName = true;
            }

            string projectBasedRuleSetName = Path.GetFileNameWithoutExtension(this.initializedProject.FullName);
            foreach (RuleSetDeclaration singleRuleSetInfo in ruleSetsInfo)
            {
                string targetRuleSetName = projectBasedRuleSetName;
                string currentRuleSetValue = useSameTargetName ? sameRuleSetCandidate : singleRuleSetInfo.RuleSetPath;

                if (!useSameTargetName && !ShouldIgnoreConfigureRuleSetValue(currentRuleSetValue))
                {
                    targetRuleSetName = string.Join(".", targetRuleSetName, singleRuleSetInfo.ConfigurationContext);
                }

                this.propertyInformationMap[singleRuleSetInfo.DeclaringProperty] = new PropertyInformation(targetRuleSetName, currentRuleSetValue);
            }
        }

        private void AddFileToProject(Project project, string fullFilePath)
        {
            Debug.Assert(Path.IsPathRooted(fullFilePath) && File.Exists(fullFilePath), "Expecting a rooted path to existing file");

            var projectSystem = this.serviceProvider.GetService<IProjectSystemHelper>();
            projectSystem.AssertLocalServiceIsNotNull();

            // Workaround for bug https://github.com/SonarSource/sonarlint-visualstudio/issues/881: Hang when binding in VS2019
            // To avoid the hang we simply don't add the ruleset file to the new SDK-style projects. However,
            // it won't make much difference in practice as the SDK-style projects use file globs so they
            // will generally include the file anyway, so the file will still appear in the Solution Explorer even
            // though it is not explicitly listed in the project file.
            if (projectSystem.IsLegacyProjectSystem(project) &&
                !projectSystem.IsFileInProject(project, fullFilePath))
            {
                projectSystem.AddFileToProject(project, fullFilePath);
            }
        }

        /// <summary>
        /// Data class that exposes simple data that can be accessed from any thread.
        /// The class itself is not thread safe and assumes only one thread accessing it at any given time.
        /// </summary>
        internal class PropertyInformation
        {
            public PropertyInformation(string targetRuleSetName, string currentRuleSetFilePath)
            {
                if (string.IsNullOrWhiteSpace(targetRuleSetName))
                {
                    throw new ArgumentNullException(nameof(targetRuleSetName));
                }

                this.TargetRuleSetFileName = targetRuleSetName;
                this.CurrentRuleSetFilePath = currentRuleSetFilePath;
            }

            public string TargetRuleSetFileName { get; }

            public string CurrentRuleSetFilePath { get; }

            public string NewRuleSetFilePath { get; set; }
        }
        #endregion
    }
}