﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Visual Studio via: 
//     Edit > Paste Special > Paste JSON as Classes
//     
//     JSON generated from: /DefaultCollection/TestProject1/_apis/build/builds/1/artifacts
//
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inedo.BuildMasterExtensions.TFS.VisualStudioOnline.Model
{
    public class GetBuildArtifactsResponse
    {
        public int count { get; set; }
        public Artifact[] value { get; set; }
    }

    public class Artifact
    {
        public int id { get; set; }
        public string name { get; set; }
        public Resource resource { get; set; }
    }

    public class Resource
    {
        public string type { get; set; }
        public string data { get; set; }
        public string url { get; set; }
        public string downloadUrl { get; set; }
    }
}
