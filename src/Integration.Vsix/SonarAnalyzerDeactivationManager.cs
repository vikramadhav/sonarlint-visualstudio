﻿//-----------------------------------------------------------------------
// <copyright file="SonarAnalyzerDeactivationManager.cs" company="SonarSource SA and Microsoft Corporation">
//   Copyright (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
//   Licensed under the MIT License. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using SonarLint.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SonarLint.VisualStudio.Integration.SonarAnalyzer;

namespace SonarLint.VisualStudio.Integration.Vsix
{
    public class SonarAnalyzerDeactivationManager : IDisposable
    {
        internal /*for testing purposes*/ enum ProjectAnalyzerStatus
        {
            NoAnalyzer,
            SameVersion,
            DifferentVersion
        }

        private readonly Workspace workspace;
        private readonly IActiveSolutionBoundTracker activeSolutionBoundTracker;

        private static readonly AssemblyName AnalyzerAssemblyName =
            new AssemblyName(typeof(SonarAnalysisContext).Assembly.FullName);
        internal /*for testing purposes*/ static readonly Version AnalyzerVersion = AnalyzerAssemblyName.Version;
        internal /*for testing purposes*/ static readonly string AnalyzerName = AnalyzerAssemblyName.Name;

        internal /*for testing purposes*/ SonarAnalyzerDeactivationManager(IServiceProvider serviceProvider, Workspace workspace)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (workspace == null)
            {
                throw new ArgumentNullException(nameof(workspace));
            }

            this.workspace = workspace;
            this.activeSolutionBoundTracker = serviceProvider.GetMefService<IActiveSolutionBoundTracker>();

            SonarAnalysisContext.ShouldAnalysisBeDisabled =
                tree => ShouldAnalysisBeDisabledOnTree(tree);
        }

        public SonarAnalyzerDeactivationManager(IServiceProvider serviceProvider):
            this(serviceProvider, GetWorkspace(serviceProvider))
        {
        }

        private static Workspace GetWorkspace(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            IComponentModel componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
            return componentModel.GetService<VisualStudioWorkspace>();
        }

        private bool ShouldAnalysisBeDisabledOnTree(SyntaxTree tree)
        {
            if (tree == null)
            {
                return false;
            }

            IEnumerable<AnalyzerReference> references = workspace?.CurrentSolution?.GetDocument(tree)?.Project?.AnalyzerReferences;
            ProjectAnalyzerStatus projectAnalyzerStatus = GetProjectAnalyzerConflictStatus(references);

            return HasConflictingAnalyzerReference(projectAnalyzerStatus) ||
                this.GetIsBoundWithoutAnalyzer(projectAnalyzerStatus);
        }

        internal /*for testing purposes*/ bool GetIsBoundWithoutAnalyzer(ProjectAnalyzerStatus projectAnalyzerStatus)
        {
            return projectAnalyzerStatus == ProjectAnalyzerStatus.NoAnalyzer &&
                this.activeSolutionBoundTracker != null &&
                this.activeSolutionBoundTracker.IsActiveSolutionBound;
        }

        internal /*for testing purposes*/ static bool HasConflictingAnalyzerReference(
            ProjectAnalyzerStatus projectAnalyzerStatus)
        {
            return projectAnalyzerStatus == ProjectAnalyzerStatus.DifferentVersion;
        }

        internal /*for testing purposes*/ static ProjectAnalyzerStatus GetProjectAnalyzerConflictStatus(
            IEnumerable<AnalyzerReference> references)
        {
            if (references == null)
            {
                return ProjectAnalyzerStatus.NoAnalyzer;
            }

            List<AnalyzerReference> sameNamedAnalyzers = references
                .Where(reference => string.Equals(reference.Display, AnalyzerName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!sameNamedAnalyzers.Any())
            {
                return ProjectAnalyzerStatus.NoAnalyzer;
            }

            bool hasConflictingAnalyzer = sameNamedAnalyzers
                .Select(reference => (reference.Id as AssemblyIdentity)?.Version)
                .All(version => version != AnalyzerVersion);

            return hasConflictingAnalyzer
                ? ProjectAnalyzerStatus.DifferentVersion
                : ProjectAnalyzerStatus.SameVersion;
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.activeSolutionBoundTracker?.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }
        #endregion
    }
}