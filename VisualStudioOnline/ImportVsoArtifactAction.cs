﻿using Inedo.BuildMaster;
using Inedo.BuildMaster.Artifacts;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Extensibility.Agents;
using Inedo.BuildMaster.Web;

namespace Inedo.BuildMasterExtensions.TFS.VisualStudioOnline
{
    [ActionProperties(
        "Import Build Artifact from VS Online",
        "Downloads build output as a zip file from Visual Studio Online or TFS 2015 and imports it as a BuildMaster artifact.",
        DefaultToLocalServer = true)]
    [RequiresInterface(typeof(IFileOperationsExecuter))]
    [RequiresInterface(typeof(IRemoteZip))]
    [CustomEditor(typeof(ImportVsoArtifactActionEditor))]
    [Tag(Tags.Builds)]
    [Tag("tfs")]
    public sealed class ImportVsoArtifactAction : TfsActionBase
    {
        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        [Persistent]
        public string BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the artifact if not empty, otherwise use the build definition name.
        /// </summary>
        [Persistent]
        public string ArtifactName { get; set; }

        public override ActionDescription GetActionDescription()
        {
            var shortDesc = new ShortActionDescription("Import VS Online Build Artifact from ", new Hilite(this.TeamProject));

            var longDesc = new LongActionDescription("using ");
            if (string.IsNullOrEmpty(this.BuildNumber))
                longDesc.AppendContent("the last successful build");
            else
                longDesc.AppendContent("build ", new Hilite(this.BuildNumber));

            longDesc.AppendContent(" of ");

            if (string.IsNullOrEmpty(this.BuildDefinition))
                longDesc.AppendContent("any build definition");
            else
                longDesc.AppendContent("build definition ", new Hilite(this.BuildDefinition));

            longDesc.AppendContent(".");

            return new ActionDescription(shortDesc, longDesc);
        }

        protected override void Execute()
        {
            var configurer = this.GetExtensionConfigurer();

            VsoArtifactImporter.DownloadAndImport(
                configurer, 
                this, 
                this.TeamProject, 
                this.BuildNumber,
                this.BuildDefinition,
                new ArtifactIdentifier(this.Context.ApplicationId, this.Context.ReleaseNumber, this.Context.BuildNumber, this.Context.DeployableId, this.ArtifactName)
            );
        }
    }
}
