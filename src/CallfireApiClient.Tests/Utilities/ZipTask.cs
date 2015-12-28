using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.IO;
using System.Linq;
using Ionic.Zip;

namespace CallfireApiClient.Tests.Utilities
{
    public class ZipTask : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        [Required]
        public string ZipFileName { get; set; }

        public override bool Execute()
        {
            var fileNames = Files.Select(f => f.ItemSpec);
            Log.LogMessage(MessageImportance.Normal, "Package files into {0}", ZipFileName);
            if (File.Exists(ZipFileName))
            {
                File.Delete(ZipFileName);
            }
            var zip = new ZipFile(ZipFileName);
            foreach (var fileName in fileNames)
            {
                zip.AddFile(fileName, "/");
            }
            zip.Save();
            return true;
        }
    }
}

