using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Xml;

namespace CallfireApiClient.Tests.Utilities
{
    public class XmlTask : Task
    {
        [Required]
        public string XmlFileName { get; set; }

        [Required]
        public string XPath { get; set; }

        [Required]
        public string Value { get; set; }

        public override bool Execute()
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(XmlFileName);
                XmlNode node = doc.SelectSingleNode(XPath);
                if (node != null)
                {
                    Log.LogMessage(MessageImportance.High, "node {0}", node);
                    Log.LogMessage(MessageImportance.High, "value {0}", Value);
                    node.InnerText = Value;
                    doc.Save(XmlFileName);
                    return true;
                }
                Log.LogError("Couldn't find Xml Node by XPath {0}", XPath);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
            return false;
        }
    }
}

