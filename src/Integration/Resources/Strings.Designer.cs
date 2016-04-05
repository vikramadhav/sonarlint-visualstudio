﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SonarLint.VisualStudio.Integration.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SonarLint.VisualStudio.Integration.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bound project: {0}.
        /// </summary>
        public static string AutomationProjectBoundDescription {
            get {
                return ResourceManager.GetString("AutomationProjectBoundDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube server: {0}.
        /// </summary>
        public static string AutomationServerDescription {
            get {
                return ResourceManager.GetString("AutomationServerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube server: {0}. This server has no projects..
        /// </summary>
        public static string AutomationServerNoProjectsDescription {
            get {
                return ResourceManager.GetString("AutomationServerNoProjectsDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bind.
        /// </summary>
        public static string BindButtonText {
            get {
                return ResourceManager.GetString("BindButtonText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Binding projects.
        /// </summary>
        public static string BindingProjectsDisplayMessage {
            get {
                return ResourceManager.GetString("BindingProjectsDisplayMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Binding solution to SonarQube project: {0}.
        /// </summary>
        public static string BindingSolutionPrefixMessageFormat {
            get {
                return ResourceManager.GetString("BindingSolutionPrefixMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bound project with key &apos;{0}&apos; was not found on the connected server. Select a new SonarQube project and bind to it..
        /// </summary>
        public static string BoundProjectNotFound {
            get {
                return ResourceManager.GetString("BoundProjectNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Browser the server in your web browser..
        /// </summary>
        public static string BrowserServerMenuItemTooltip {
            get {
                return ResourceManager.GetString("BrowserServerMenuItemTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Browse.
        /// </summary>
        public static string BrowseServerMenuItemDisplayText {
            get {
                return ResourceManager.GetString("BrowseServerMenuItemDisplayText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ca_ncel.
        /// </summary>
        public static string CancelButtonText {
            get {
                return ResourceManager.GetString("CancelButtonText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cancel.
        /// </summary>
        public static string CancelLinkText {
            get {
                return ResourceManager.GetString("CancelLinkText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A SonarQube server plugin has a malformed version and cannot be compared. Version was &quot;{0}&quot;..
        /// </summary>
        public static string CannotCompareVersionStrings {
            get {
                return ResourceManager.GetString("CannotCompareVersionStrings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing rules (or Action set to None): .
        /// </summary>
        public static string ConflictDetailMissingRules {
            get {
                return ResourceManager.GetString("ConflictDetailMissingRules", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rule &apos;{0}&apos; action is {1}, instead of {2}..
        /// </summary>
        public static string ConflictDetailWeakenedRulesDetail {
            get {
                return ResourceManager.GetString("ConflictDetailWeakenedRulesDetail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Weakened rules:.
        /// </summary>
        public static string ConflictDetailWeakenedRulesTitle {
            get {
                return ResourceManager.GetString("ConflictDetailWeakenedRulesTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RuleSet conflicts resolution summary for project &apos;{0}&apos;, configuration(s): {1}.
        /// </summary>
        public static string ConflictFixHeader {
            get {
                return ResourceManager.GetString("ConflictFixHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RuleSet imports were reset for: &apos;{0}&apos;..
        /// </summary>
        public static string ConflictFixResetInclude {
            get {
                return ResourceManager.GetString("ConflictFixResetInclude", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Conflicting rules were removed from &apos;{0}&apos;:.
        /// </summary>
        public static string ConflictFixRulesDeleted {
            get {
                return ResourceManager.GetString("ConflictFixRulesDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RuleSet &apos;{0}&apos; cannot be analyzed against &apos;{1}&apos; due to error: {2}. .
        /// </summary>
        public static string ConflictsManagerFailedInFindConflicts {
            get {
                return ResourceManager.GetString("ConflictsManagerFailedInFindConflicts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarLint conflicts manager warning: {0}.
        /// </summary>
        public static string ConflictsManagerWarningMessage {
            get {
                return ResourceManager.GetString("ConflictsManagerWarningMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RuleSet conflicts detected for project &apos;{0}&apos;, configuration(s): {1}.
        /// </summary>
        public static string ConflictsSummaryHeader {
            get {
                return ResourceManager.GetString("ConflictsSummaryHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to _Connect.
        /// </summary>
        public static string ConnectButtonText {
            get {
                return ResourceManager.GetString("ConnectButtonText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connect to a SonarQube Server.
        /// </summary>
        public static string ConnectDialogTitle {
            get {
                return ResourceManager.GetString("ConnectDialogTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connecting to {0}.
        /// </summary>
        public static string ConnectingToSever {
            get {
                return ResourceManager.GetString("ConnectingToSever", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connecting to SonarQube server: {0}.
        /// </summary>
        public static string ConnectingToSonarQubePrefixMessageFormat {
            get {
                return ResourceManager.GetString("ConnectingToSonarQubePrefixMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot connect to the SonarQube server. Make sure you provided the correct connection information, including your sign-in credentials, and [try again](). 
        ///See more information in the output window..
        /// </summary>
        public static string ConnectionFailed {
            get {
                return ResourceManager.GetString("ConnectionFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Canceled.
        /// </summary>
        public static string ConnectionResultCancellation {
            get {
                return ResourceManager.GetString("ConnectionResultCancellation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed.
        /// </summary>
        public static string ConnectionResultFailure {
            get {
                return ResourceManager.GetString("ConnectionResultFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Succeeded.
        /// </summary>
        public static string ConnectionResultSuccess {
            get {
                return ResourceManager.GetString("ConnectionResultSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connect....
        /// </summary>
        public static string ConnectLinkText {
            get {
                return ResourceManager.GetString("ConnectLinkText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connections.
        /// </summary>
        public static string ConnectSectionTitle {
            get {
                return ResourceManager.GetString("ConnectSectionTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning: could not find CodeAnalysisRuleSet for project &apos;{0}&apos;.
        /// </summary>
        public static string CouldNotFindCodeAnalysisRuleSetPropertyOnProject {
            get {
                return ResourceManager.GetString("CouldNotFindCodeAnalysisRuleSetPropertyOnProject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to C#.
        /// </summary>
        public static string CSharpLanguageName {
            get {
                return ResourceManager.GetString("CSharpLanguageName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disconnect.
        /// </summary>
        public static string DisconnectCommandDisplayText {
            get {
                return ResourceManager.GetString("DisconnectCommandDisplayText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disconnect from SonarQube project.
        /// </summary>
        public static string DisconnectCommandTooltip {
            get {
                return ResourceManager.GetString("DisconnectCommandTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Discovering solution projects.
        /// </summary>
        public static string DiscoveringSolutionProjectsProgressMessage {
            get {
                return ResourceManager.GetString("DiscoveringSolutionProjectsProgressMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Download quality profile for language &apos;{0}&apos;.
        /// </summary>
        public static string DownloadingQualityProfileProgressMessage {
            get {
                return ResourceManager.GetString("DownloadingQualityProfileProgressMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Installing NuGet package {0} for project: {1}.
        /// </summary>
        public static string EnsuringNugetPackagesProgressMessage {
            get {
                return ResourceManager.GetString("EnsuringNugetPackagesProgressMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Insecure schemes must be a subset of supported schemes.
        /// </summary>
        public static string ExceptionInsecureSchemesIsNotSubset {
            get {
                return ResourceManager.GetString("ExceptionInsecureSchemesIsNotSubset", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target URI must be an absolute URI.
        /// </summary>
        public static string ExpectedAbsoluteUris {
            get {
                return ResourceManager.GetString("ExpectedAbsoluteUris", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The expected RuleSet file &apos;{0}&apos; could not be resolved or the file does not exist. Cannot search conflicts for one or more project configuration of project &apos;{1}&apos;. Please rebind to the relevant SonarQube project from TeamExplorer window..
        /// </summary>
        public static string ExpectedRuleSetNotFound {
            get {
                return ResourceManager.GetString("ExpectedRuleSetNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to install NuGet package &apos;{0}&apos; for project &apos;{1}&apos;. Message: {2}.
        /// </summary>
        public static string FailedDuringNuGetPackageInstall {
            get {
                return ResourceManager.GetString("FailedDuringNuGetPackageInstall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to delete credentials for {0}..
        /// </summary>
        public static string FailedToDeleteCredentials {
            get {
                return ResourceManager.GetString("FailedToDeleteCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed loading file: &apos;{0}&apos;. Please rebind solution to create a new configuration file..
        /// </summary>
        public static string FailedToDeserializeSQCOnfiguration {
            get {
                return ResourceManager.GetString("FailedToDeserializeSQCOnfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to bind the solution to SonarQube project, [try again](). 
        ///See more information in the output window..
        /// </summary>
        public static string FailedToToBindSolution {
            get {
                return ResourceManager.GetString("FailedToToBindSolution", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to unpack additional files required for analyzers.
        /// </summary>
        public static string FailedToUnpackAdditionalFiles {
            get {
                return ResourceManager.GetString("FailedToUnpackAdditionalFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not all NuGet packages were installed. Please see output above for more information..
        /// </summary>
        public static string FinishedSolutionBindingWorkflowNotAllPackagesInstalled {
            get {
                return ResourceManager.GetString("FinishedSolutionBindingWorkflowNotAllPackagesInstalled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Completed successfully.
        /// </summary>
        public static string FinishedSolutionBindingWorkflowSuccessful {
            get {
                return ResourceManager.GetString("FinishedSolutionBindingWorkflowSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hide Unbound Projects.
        /// </summary>
        public static string HideUnboundProjectsCommandText {
            get {
                return ResourceManager.GetString("HideUnboundProjectsCommandText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sonar Qube rule set import: Sonar Qube rule with key &apos;{0}&apos; was ignored since it&apos;s missing an internal key. Contact your Sonar Qube administrator..
        /// </summary>
        public static string IgnoredSonarQubeRuleDueToMissingInternalKey {
            get {
                return ResourceManager.GetString("IgnoredSonarQubeRuleDueToMissingInternalKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Different CodeAnalysisRuleSetDirectories value &apos;{0}&apos; for RuleSet &apos;{1}&apos;. Using value &apos;{2}&apos;..
        /// </summary>
        public static string InconsistentRuleSetDirectoriesWarning {
            get {
                return ResourceManager.GetString("InconsistentRuleSetDirectoriesWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The URL you entered is not secure. Use HTTPS connections to protect sensitive information..
        /// </summary>
        public static string InsecureProtocolWarning {
            get {
                return ResourceManager.GetString("InsecureProtocolWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Colon (&quot;:&quot;) is not a permitted character..
        /// </summary>
        public static string InvalidCharacterColon {
            get {
                return ResourceManager.GetString("InvalidCharacterColon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{0}&quot; is not a valid URL.
        /// </summary>
        public static string InvalidServerUriFormat {
            get {
                return ResourceManager.GetString("InvalidServerUriFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test project regular expression pattern &apos;{0}&apos; is invalid. The default will be used instead. Please check your server settings..
        /// </summary>
        public static string InvalidTestProjectRegexPattern {
            get {
                return ResourceManager.GetString("InvalidTestProjectRegexPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not acquire service: {0}.
        /// </summary>
        public static string MissingService {
            get {
                return ResourceManager.GetString("MissingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to More info....
        /// </summary>
        public static string MoreInfoLinkText {
            get {
                return ResourceManager.GetString("MoreInfoLinkText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No projects in the current solution are applicable to the selected SonarQube project&apos;s quality profile..
        /// </summary>
        public static string NoProjectsApplicableForBinding {
            get {
                return ResourceManager.GetString("NoProjectsApplicableForBinding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field can not be empty.
        /// </summary>
        public static string NotEmptyValidatorRequiredField {
            get {
                return ResourceManager.GetString("NotEmptyValidatorRequiredField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password:.
        /// </summary>
        public static string PasswordLabel {
            get {
                return ResourceManager.GetString("PasswordLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An absolute file path was expected..
        /// </summary>
        public static string PathHelperAbsolutePathExpected {
            get {
                return ResourceManager.GetString("PathHelperAbsolutePathExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Preparing binding steps.
        /// </summary>
        public static string PreparingBindingWorkflowProgessMessage {
            get {
                return ResourceManager.GetString("PreparingBindingWorkflowProgessMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarSource and Microsoft.
        /// </summary>
        public static string ProductAuthors {
            get {
                return ResourceManager.GetString("ProductAuthors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not get the IVsHierarchy/IVsBuildPropertyStorage for the given project is IVsBuildPropertyStorage for the given EnvDTE.Project.
        /// </summary>
        public static string ProjectFilterDteProjectFailedToGetIVs {
            get {
                return ResourceManager.GetString("ProjectFilterDteProjectFailedToGetIVs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Key: {0}.
        /// </summary>
        public static string ProjectToolTipKeyFormat {
            get {
                return ResourceManager.GetString("ProjectToolTipKeyFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} (Bound).
        /// </summary>
        public static string ProjectToolTipProjectNameFormat {
            get {
                return ResourceManager.GetString("ProjectToolTipProjectNameFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube quality profile was downloaded successfully.
        /// </summary>
        public static string QualityProfileDownloadedSuccessfulMessage {
            get {
                return ResourceManager.GetString("QualityProfileDownloadedSuccessfulMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube quality profile: Failed to download profile.
        /// </summary>
        public static string QualityProfileDownloadFailedMessage {
            get {
                return ResourceManager.GetString("QualityProfileDownloadFailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refresh.
        /// </summary>
        public static string RefreshCommandDisplayText {
            get {
                return ResourceManager.GetString("RefreshCommandDisplayText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Will refetch the SonarQube projects from the server using the existing credentials.
        /// </summary>
        public static string RefreshCommandTooltip {
            get {
                return ResourceManager.GetString("RefreshCommandTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The applied ruleset is weakening the SonarQube quality profile in one or more projects. See output window (General) for more details. [Click here]() to fix automatically..
        /// </summary>
        public static string RuleSetConflictsDetected {
            get {
                return ResourceManager.GetString("RuleSetConflictsDetected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rule set generated from SonarQube.
        /// </summary>
        public static string RuleSetDescription {
            get {
                return ResourceManager.GetString("RuleSetDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generating project rule sets.
        /// </summary>
        public static string RuleSetGenerationProgressMessage {
            get {
                return ResourceManager.GetString("RuleSetGenerationProgressMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a SonarQube project and bind it to the current solution..
        /// </summary>
        public static string SelectSonarQubeProjectInstruction {
            get {
                return ResourceManager.GetString("SelectSonarQubeProjectInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified server does not have the minimum required version ({0}) of the C# plugin installed. Please contact your SonarQube server administrator..
        /// </summary>
        public static string ServerDoesNotHaveCorrectVersionOfCSharpPlugin {
            get {
                return ResourceManager.GetString("ServerDoesNotHaveCorrectVersionOfCSharpPlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (no projects).
        /// </summary>
        public static string ServerNoProjectsInlineText {
            get {
                return ResourceManager.GetString("ServerNoProjectsInlineText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This server has no available projects with which to bind to. Please create at least one project and then refresh this connection..
        /// </summary>
        public static string ServerNoProjectsToolTipText {
            get {
                return ResourceManager.GetString("ServerNoProjectsToolTipText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NuGet analyzer packages may be downloaded and installed automatically when you bind to a SonarQube project.
        ///[Don&apos;t warn me again]().
        /// </summary>
        public static string ServerNuGetTrustWarningMessage {
            get {
                return ResourceManager.GetString("ServerNuGetTrustWarningMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show All Projects.
        /// </summary>
        public static string ShowAllProjectsCommandText {
            get {
                return ResourceManager.GetString("ShowAllProjectsCommandText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show a warning about automatically downloading NuGet packages when establishing a SonarQube server connection..
        /// </summary>
        public static string ShowServerNuGetTrustWarningSettingDescription {
            get {
                return ResourceManager.GetString("ShowServerNuGetTrustWarningSettingDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show NuGet package warnings.
        /// </summary>
        public static string ShowServerNuGetTrustWarningSettingName {
            get {
                return ResourceManager.GetString("ShowServerNuGetTrustWarningSettingName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot bind or sync if there are unsaved changes. Operation cancelled..
        /// </summary>
        public static string SolutionSaveCancelledBindAborted {
            get {
                return ResourceManager.GetString("SolutionSaveCancelledBindAborted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube is an open source platform to manage code quality. Connect your solution to an existing SonarQube Server to get the same issues in Visual Studio and in your SonarQube server..
        /// </summary>
        public static string SonarQubeDescription {
            get {
                return ResourceManager.GetString("SonarQubeDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube.
        /// </summary>
        public static string SonarQubeName {
            get {
                return ResourceManager.GetString("SonarQubeName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube request failed: {0} {1}.
        /// </summary>
        public static string SonarQubeRequestFailed {
            get {
                return ResourceManager.GetString("SonarQubeRequestFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube request timed out or was cancelled.
        /// </summary>
        public static string SonarQubeRequestTimeoutOrCancelled {
            get {
                return ResourceManager.GetString("SonarQubeRequestTimeoutOrCancelled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube server:.
        /// </summary>
        public static string SonarQubeServerLabel {
            get {
                return ResourceManager.GetString("SonarQubeServerLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Started.
        /// </summary>
        public static string StartedSolutionBindingWorkflow {
            get {
                return ResourceManager.GetString("StartedSolutionBindingWorkflow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sync.
        /// </summary>
        public static string SyncButtonText {
            get {
                return ResourceManager.GetString("SyncButtonText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube.
        /// </summary>
        public static string TeamExplorerPageTitle {
            get {
                return ResourceManager.GetString("TeamExplorerPageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Toggle visibility of unbound projects for this server..
        /// </summary>
        public static string ToggleShowAllProjectsCommandTooltip {
            get {
                return ResourceManager.GetString("ToggleShowAllProjectsCommandTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: Unexpected error: {1}. You can help us improve by reporting this bug at {2}..
        /// </summary>
        public static string UnexpectedErrorMessageFormat {
            get {
                return ResourceManager.GetString("UnexpectedErrorMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected token type encountered.
        /// </summary>
        public static string UnexpectedTokenType {
            get {
                return ResourceManager.GetString("UnexpectedTokenType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected error during workflow execution: {0}..
        /// </summary>
        public static string UnexpectedWorkflowError {
            get {
                return ResourceManager.GetString("UnexpectedWorkflowError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unpacking additional file: {0}.
        /// </summary>
        public static string UnpackingAdditionalFile {
            get {
                return ResourceManager.GetString("UnpackingAdditionalFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username:.
        /// </summary>
        public static string UsernameLabel {
            get {
                return ResourceManager.GetString("UsernameLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If a password is specified, a username must also be specified..
        /// </summary>
        public static string UsernameRequired {
            get {
                return ResourceManager.GetString("UsernameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VB.NET.
        /// </summary>
        public static string VBNetLanguageName {
            get {
                return ResourceManager.GetString("VBNetLanguageName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to View in SonarQube.
        /// </summary>
        public static string ViewInSonarQubeMenuItemDisplayText {
            get {
                return ResourceManager.GetString("ViewInSonarQubeMenuItemDisplayText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open the dashboard for this project in SonarQube using your web browser..
        /// </summary>
        public static string ViewInSonarQubeMenuItemTooltip {
            get {
                return ResourceManager.GetString("ViewInSonarQubeMenuItemTooltip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warnings.
        /// </summary>
        public static string WarningsSettingsCategory {
            get {
                return ResourceManager.GetString("WarningsSettingsCategory", resourceCulture);
            }
        }
    }
}
