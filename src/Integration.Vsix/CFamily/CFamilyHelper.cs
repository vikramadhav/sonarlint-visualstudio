﻿/*
 * SonarLint for Visual Studio
 * Copyright (C) 2016-2021 SonarSource SA
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
using Newtonsoft.Json;
using SonarLint.VisualStudio.Core.Analysis;
using SonarLint.VisualStudio.Core.CFamily;

namespace SonarLint.VisualStudio.Integration.Vsix.CFamily
{
    internal static partial class CFamilyHelper
    {
        public const string CPP_LANGUAGE_KEY = "cpp";
        public const string C_LANGUAGE_KEY = "c";

        internal static string WorkingDirectory => Path.GetTempPath();
        internal static string PchFilePath => Path.Combine(WorkingDirectory, "SonarLintForVisualStudio.PCH.preamble");
        internal static string RequestConfigFilePath => Path.Combine(WorkingDirectory, "sonar-cfamily.request.reproducer");
        internal static string ReproducerFilePath => Path.Combine(WorkingDirectory, "sonar-cfamily.reproducer");

        internal static IFileSystem FileSystem { get; set; } = new FileSystem();

        public static readonly string CFamilyFilesDirectory = Path.Combine(
            Path.GetDirectoryName(typeof(CFamilyHelper).Assembly.Location),
            "lib");

        private static readonly string analyzerExeFilePath = Path.Combine(
            CFamilyFilesDirectory, "subprocess.exe");

        internal class NoOpLogger : ILogger
        {
            public void WriteLine(string message)
            {
            }

            public void WriteLine(string messageFormat, params object[] args)
            {
            }
        }

        private static readonly NoOpLogger noOpLogger = new NoOpLogger();

        public static Request CreateRequest(ILogger logger, ProjectItem projectItem, string absoluteFilePath, ICFamilyRulesConfigProvider cFamilyRulesConfigProvider, IAnalyzerOptions analyzerOptions)
        {
            if (analyzerOptions is CFamilyAnalyzerOptions cFamilyAnalyzerOptions && cFamilyAnalyzerOptions.CreatePreCompiledHeaders)
            {
                // In case the requeset is coming from PCH generation, we don't log failures.
                // This is to avoid redundant messages while navigating unsupoported files.
                logger = noOpLogger;
            }

            if (!IsFileInSolution(projectItem))
            {
                logger.WriteLine($"Unable to retrieve the configuration for file '{absoluteFilePath}'. Check the file is part of a project in the current solution.");
                return null;
            }

            var fileConfig = TryGetConfig(logger, projectItem, absoluteFilePath, analyzerOptions);
            if (fileConfig == null)
            {
                return null;
            }

            return CreateRequest(fileConfig, absoluteFilePath, cFamilyRulesConfigProvider, analyzerOptions);
        }

        private static Request CreateRequest(FileConfig fileConfig, string absoluteFilePath, ICFamilyRulesConfigProvider cFamilyRulesConfigProvider, IAnalyzerOptions analyzerOptions)
        {
            var request = ToRequest(fileConfig, absoluteFilePath);
            if (request?.File == null || request?.CFamilyLanguage == null)
            {
                return null;
            }

            request.PchFile = PchFilePath;

            bool isPCHBuild = false;

            if (analyzerOptions is CFamilyAnalyzerOptions cFamilyAnalyzerOptions)
            {
                Debug.Assert(!(cFamilyAnalyzerOptions.CreateReproducer && cFamilyAnalyzerOptions.CreatePreCompiledHeaders), "Only one flag (CreateReproducer, CreatePreCompiledHeaders) can be set at a time");
                
                if (cFamilyAnalyzerOptions.CreateReproducer)
                {
                    request.Flags |= Request.CreateReproducer;
                }

                if (cFamilyAnalyzerOptions.CreatePreCompiledHeaders)
                {
                    request.Flags |= Request.BuildPreamble;
                    isPCHBuild = true;
                }
            }

            if (!isPCHBuild)
            {
                // We don't need to calculate / set the rules configuration for PCH builds
                request.RulesConfiguration = cFamilyRulesConfigProvider.GetRulesConfiguration(request.CFamilyLanguage);
                Debug.Assert(request.RulesConfiguration != null, "RulesConfiguration should be set for the analysis request");
                request.Options = GetKeyValueOptionsList(request.RulesConfiguration);
            }

            return request;
        }

        private static void SaveFileConfig(FileConfig fileConfig, ILogger logger)
        {
            var serializedFileConfig = JsonConvert.SerializeObject(fileConfig);
            FileSystem.File.WriteAllText(RequestConfigFilePath, serializedFileConfig);
            logger.WriteLine(CFamilyStrings.MSG_RequestConfigSaved, RequestConfigFilePath);
        }

        internal /* for testing */ static string[]GetKeyValueOptionsList(ICFamilyRulesConfig rulesConfiguration)
        {
            var options = GetDefaultOptions(rulesConfiguration);
            options.Add("internal.qualityProfile", string.Join(",", rulesConfiguration.ActivePartialRuleKeys));
            var data = options.Select(kv => kv.Key + "=" + kv.Value).ToArray();
            return data;
        }

        private static Dictionary<string, string> GetDefaultOptions(ICFamilyRulesConfig rulesConfiguration)
        {
            Dictionary<string, string> defaults = new Dictionary<string, string>();
            foreach (string ruleKey in rulesConfiguration.AllPartialRuleKeys)
            {
                if (rulesConfiguration.RulesParameters.TryGetValue(ruleKey, out IDictionary<string, string> ruleParams))
                {
                    foreach (var param in ruleParams)
                    {
                        string optionKey = ruleKey + "." + param.Key;
                        defaults.Add(optionKey, param.Value);
                    }
                }
            }

            return defaults;
        }

        internal /* for testing */ static void CallClangAnalyzer(Action<Message> handleMessage, Request request, IProcessRunner runner, ILogger logger, CancellationToken cancellationToken)
        {
            if (analyzerExeFilePath == null)
            {
                logger.WriteLine("Unable to locate the CFamily analyzer exe");
                return;
            }

            const string communicateViaStreaming = "-"; // signal the subprocess we want to communicate via standard IO streams.

            var args = new ProcessRunnerArguments(analyzerExeFilePath, false)
            {
                CmdLineArgs = new[] { communicateViaStreaming },
                CancellationToken = cancellationToken,
                WorkingDirectory = WorkingDirectory,
                HandleInputStream = writer =>
                {
                    using (var binaryWriter = new BinaryWriter(writer.BaseStream))
                    {
                        Protocol.Write(binaryWriter, request);
                    }
                },
                HandleOutputStream = reader =>
                {
                    if ((request.Flags & Request.CreateReproducer) != 0)
                    {
                        reader.ReadToEnd();
                        logger.WriteLine(CFamilyStrings.MSG_ReproducerSaved, ReproducerFilePath);
                    }
                    else if ((request.Flags & Request.BuildPreamble) != 0)
                    {
                        reader.ReadToEnd();
                        logger.WriteLine(CFamilyStrings.MSG_PchSaved, request.File, request.PchFile);
                    }
                    else
                    {
                        using (var binaryReader = new BinaryReader(reader.BaseStream))
                        {
                            Protocol.Read(binaryReader, handleMessage, request.File);
                        }
                    }
                }
            };

            runner.Execute(args);
        }
  
        internal static FileConfig TryGetConfig(ILogger logger, ProjectItem projectItem, string absoluteFilePath, IAnalyzerOptions analyzerOptions)
        {

            Debug.Assert(IsFileInSolution(projectItem),
                $"Not expecting to be called for files that are not in the solution: {absoluteFilePath}");

            try
            {
                // Note: if the C++ tools are not installed then it's likely an exception will be thrown when
                // the framework tries to JIT-compile the TryGet method (since it won't be able to find the MS.VS.VCProjectEngine
                // types).
                var fileConfig = FileConfig.TryGet(logger, projectItem, absoluteFilePath);

                if (analyzerOptions is CFamilyAnalyzerOptions cFamilyAnalyzerOptions &&
                    cFamilyAnalyzerOptions.CreateReproducer)
                {
                    SaveFileConfig(fileConfig, logger);
                }

                return fileConfig;
            }
            catch (Exception e)
            {
                logger.WriteLine($"Unable to collect C/C++ configuration for {absoluteFilePath}: {e}");
                return null;
            }
        }

        internal static bool IsFileInSolution(ProjectItem projectItem)
        {
            try
            {
                // Issue 667:  https://github.com/SonarSource/sonarlint-visualstudio/issues/667
                // If you open a C++ file that is not part of the current solution then
                // VS will cruft up a temporary vcxproj so that it can provide language
                // services for the file (e.g. syntax highlighting). This means that
                // even though we have what looks like a valid project item, it might
                // not actually belong to a real project.
                var indexOfSingleFileString = projectItem?.ContainingProject?.FullName.IndexOf("SingleFileISense", StringComparison.OrdinalIgnoreCase);
                return indexOfSingleFileString.HasValue &&
                    indexOfSingleFileString.Value <= 0 &&
                    projectItem.ConfigurationManager != null &&
                    // the next line will throw if the file is not part of a solution
                    projectItem.ConfigurationManager.ActiveConfiguration != null;
            }
            catch (Exception ex) when (!Microsoft.VisualStudio.ErrorHandler.IsCriticalException(ex))
            {
                // Suppress non-critical exceptions
            }
            return false;
        }

        private static Request ToRequest(FileConfig fileConfig, string path)
        {
            Capture[] c = CFamilyHelper.Capture.ToCaptures(fileConfig, path, out string cfamilyLanguage);
            var request = MsvcDriver.ToRequest(c);
            request.CFamilyLanguage = cfamilyLanguage;
            if (fileConfig.ItemType == "ClInclude")
            {
                request.Flags |= Request.MainFileIsHeader;
            }
            return request;
        }

        private static void AddRange(IList<string> cmd, string[] values)
        {
            foreach (string value in values)
            {
                Add(cmd, value);
            }
        }

        private static void AddRange(IList<string> cmd, string prefix, string[] values)
        {
            foreach (string value in values)
            {
                Add(cmd, prefix, value);
            }
        }

        private static void Add(IList<String> cmd, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                cmd.Add(value);
            }
        }

        private static void Add(IList<string> cmd, string prefix, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                cmd.Add(prefix);
                cmd.Add(value);
            }
        }
    }
}
