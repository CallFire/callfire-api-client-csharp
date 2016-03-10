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

        public string AddValueAsChildNode { get; set; }

        public override bool Execute()
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(XmlFileName);
                XmlNode node = doc.SelectSingleNode(XPath);
                if (node != null)
                {
                    if (AddValueAsChildNode == "true")
                    {
                        Log.LogMessage(MessageImportance.Normal, "appending {0} node by node with value {1}", node.Name, Value);
                        XmlDocument docToAdd = new XmlDocument();
                        docToAdd.LoadXml(Value);

                        XmlNodeList childNodes = node.ChildNodes;
                        for (int i = childNodes.Count - 1; i >= 0; i--)
                        {
                            if (childNodes[i].OuterXml == Value)
                            {
                                Log.LogMessage(MessageImportance.Normal, "Removing item from node");
                                childNodes[i].ParentNode.RemoveChild(childNodes[i]);
                            }
                        }
                        doc.Save(XmlFileName);

                        XmlNode importNode = node.OwnerDocument.ImportNode(docToAdd.SelectSingleNode("/*"), true);
                        node.AppendChild(importNode);
                    }
                    else
                    {
                        Log.LogMessage(MessageImportance.Normal, "updating {0} node's value to {1}", node.Name, Value);
                        node.InnerText = Value;
                    }
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

