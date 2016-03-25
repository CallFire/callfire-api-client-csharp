using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Reflection;
using System.IO;

namespace CallfireApiClient.Tests.Utilities
{
    public class VersionTask : Task
    {
        [Required]
        public string AssemblyPath { get; set; }

        [Output]
        public string Version { get; set; }

        public override bool Execute()
        {
            try
            {
                var _version = Assembly.LoadFile(Path.GetFullPath(AssemblyPath)).GetName().Version;
                Version = string.Format("{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                return false;
            }
            return true;
        }
    }
}

