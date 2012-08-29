using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace IBS.Utilities.ASMWSTester.XmlConfig
{
    //TODO refactory it
    public static class XmlCongifManager
    {
        public static void ReadConfig(TreeNode treeNode, XmlNode xmlNode, ref XmlDocument xmlDocument)
        {
            XmlElement node1 = null;

            if (treeNode.Tag != null)
            {
                string nodeType = treeNode.Tag.GetType().ToString();

                switch (nodeType)
                {
                    case "IBS.Utilities.ASMWSTester.MethodProperty":
                        node1 = xmlDocument.CreateElement(treeNode.Text);

                        if (treeNode.Text != null)
                        {
                            xmlDocument.AppendChild(node1);
                        }
                        break;
                    case "IBS.Utilities.ASMWSTester.ClassProperty":
                        node1 = xmlDocument.CreateElement(((ClassProperty) treeNode.Tag).Name);

                        if (((ClassProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        xmlNode.AppendChild(node1);
                        break;
                    case "IBS.Utilities.ASMWSTester.PrimitiveProperty":
                        node1 = xmlDocument.CreateElement(((PrimitiveProperty) treeNode.Tag).Name);

                        if (((PrimitiveProperty) treeNode.Tag).ReadChildren() != null)
                        {
                            node1.SetAttribute("value", ((PrimitiveProperty) treeNode.Tag).ReadChildren().ToString());
                        }

                        if (((PrimitiveProperty) treeNode.Tag).Type != null)
                        {
                            node1.SetAttribute("type", ((PrimitiveProperty) treeNode.Tag).Type.ToString());
                        }

                        xmlNode.AppendChild(node1);
                        break;
                    case "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty":
                        node1 = xmlDocument.CreateElement(((NullablePrimitiveProperty) treeNode.Tag).Name);

                        if (((NullablePrimitiveProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        if (((NullablePrimitiveProperty) treeNode.Tag).ReadChildren() != null)
                        {
                            node1.SetAttribute("value",
                                               ((NullablePrimitiveProperty) treeNode.Tag).ReadChildren().ToString());
                        }

                        if (((NullablePrimitiveProperty) treeNode.Tag).Type != null)
                        {
                            node1.SetAttribute("type", ((NullablePrimitiveProperty) treeNode.Tag).Type.ToString());
                        }

                        xmlNode.AppendChild(node1);
                        break;
                    case "IBS.Utilities.ASMWSTester.ArrayProperty":
                        node1 = xmlDocument.CreateElement(((ArrayProperty) treeNode.Tag).Name);

                        if (((ArrayProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        node1.SetAttribute("length", ((ArrayProperty) treeNode.Tag).Length.ToString());

                        xmlNode.AppendChild(node1);
                        break;
                    default:
                        throw new NotSupportedException("Not Supported Property:" + nodeType);
                }
            }
            else
            {
                node1 = xmlDocument.CreateElement(treeNode.Text);

                if (treeNode.Text != null)
                {
                    xmlNode.AppendChild(node1);
                }
            }

            foreach (TreeNode childnode in treeNode.Nodes)
            {
                ReadConfig(childnode, node1, ref xmlDocument);
            }
        }

        public static void ApplyConfig(TreeNode treeNode, string currentxpath, XmlDocument xmlDocument,
                                       Assembly proxyAssembly)
        {
            if (treeNode.Tag != null)
            {
                string nodeType = treeNode.Tag.GetType().ToString();

                XmlNode currentNode = null;
                switch (nodeType)
                {
                    case "IBS.Utilities.ASMWSTester.MethodProperty":
                        currentxpath += ((MethodProperty) treeNode.Tag).Name;
                        //((MethodProperty)treeNode.Tag). = xmlDocument.SelectSingleNode(currentxpath);

                        break;
                    case "IBS.Utilities.ASMWSTester.ClassProperty":
                        currentxpath += "/" + ((ClassProperty) treeNode.Tag).Name;

                        currentNode = xmlDocument.SelectSingleNode(currentxpath);
                        if (currentNode != null)
                        {
                            if (currentNode.Attributes["isNull"] != null)
                            {
                                ((ClassProperty) treeNode.Tag).IsNull =
                                    Convert.ToBoolean(currentNode.Attributes["isNull"].Value);
                            }
                            else
                            {
                                if (currentNode.Attributes["type"] != null)
                                {
                                    if (Type.GetType(currentNode.Attributes["type"].Value)
                                        != ((ClassProperty) treeNode.Tag).Type)
                                    {
                                        ((ClassProperty) treeNode.Tag).Type =
                                            Type.GetType(currentNode.Attributes["type"].Value);
                                        TreeNodeProperty property1 = treeNode.Tag as TreeNodeProperty;
                                        TreeNodeProperty property2 = TreeNodeProperty.CreateTreeNodeProperty(property1);
                                        property2.TreeNode = property1.TreeNode;
                                        property2.RecreateSubtree(null);
                                        treeNode = property2.TreeNode;


                                        if (currentNode.Attributes["value"] != null)
                                        {
                                            Type type = Type.GetType(currentNode.Attributes["type"].Value);

                                            if (type == null)
                                            {
                                                type = proxyAssembly.GetType(currentNode.Attributes["type"].Value);
                                            }

                                            if (type.IsEnum)
                                            {
                                                ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                                    Enum.Parse(type, currentNode.Attributes["value"].Value);
                                            }
                                            else
                                            {
                                                ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                                    Convert.ChangeType(currentNode.Attributes["value"].Value, type);
                                            }
                                        }
                                    }
                                }


                                ((ClassProperty) treeNode.Tag).IsNull = false;
                            }
                        }
                        break;
                    case "IBS.Utilities.ASMWSTester.PrimitiveProperty":
                        currentxpath += "/" + ((PrimitiveProperty) treeNode.Tag).Name;

                        currentNode = xmlDocument.SelectSingleNode(currentxpath);

                        if (currentNode != null)
                        {
                            Type type = Type.GetType(currentNode.Attributes["type"].Value);

                            if (type == null)
                            {
                                type = proxyAssembly.GetType(currentNode.Attributes["type"].Value);
                            }

                            if (type.IsEnum)
                            {
                                ((PrimitiveProperty) treeNode.Tag).Value =
                                    Enum.Parse(type, currentNode.Attributes["value"].Value);
                            }
                            else
                            {
                                ((PrimitiveProperty) treeNode.Tag).Value =
                                    Convert.ChangeType(currentNode.Attributes["value"].Value, type);
                            }
                        }

                        break;
                    case "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty":
                        currentxpath += "/" + ((NullablePrimitiveProperty) treeNode.Tag).Name;

                        currentNode = xmlDocument.SelectSingleNode(currentxpath);
                        if (currentNode != null)
                        {
                            if (currentNode.Attributes["isNull"] != null)
                            {
                                ((NullablePrimitiveProperty) treeNode.Tag).IsNull =
                                    Convert.ToBoolean(currentNode.Attributes["isNull"].Value);
                            }
                            else
                            {
                                ((NullablePrimitiveProperty) treeNode.Tag).IsNull = false;
                            }

                            //if (currentNode.Attributes["type"] != null)
                            //{
                                Type type = Type.GetType(currentNode.Attributes["type"].Value);

                                if (type == null)
                                {
                                    type = proxyAssembly.GetType(currentNode.Attributes["type"].Value);
                                }

                                if (type.IsEnum)
                                {
                                    if (currentNode.Attributes["value"] != null)
                                    {
                                        ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                            Enum.Parse(type, currentNode.Attributes["value"].Value);
                                    }
                                }
                                else
                                {
                                    if (currentNode.Attributes["value"] != null)
                                    {
                                        ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                            Convert.ChangeType(currentNode.Attributes["value"].Value, type);
                                    }
                                }
                            //}
                        }
                        break;
                    case "IBS.Utilities.ASMWSTester.ArrayProperty":
                        currentxpath += "/" + ((ArrayProperty) treeNode.Tag).Name;
                        currentNode = xmlDocument.SelectSingleNode(currentxpath);

                        if (currentNode.Attributes["isNull"] != null)
                        {
                            ((ArrayProperty) treeNode.Tag).IsNull =
                                Convert.ToBoolean(currentNode.Attributes["isNull"].Value);
                        }
                        else
                        {
                            ((ArrayProperty) treeNode.Tag).IsNull = false;
                        }
                        ((ArrayProperty) treeNode.Tag).Length = Convert.ToInt32(
                            currentNode.Attributes["length"].Value);
                        break;
                    default:
                        throw new NotSupportedException("Not Supported Property:" + nodeType);
                }
            }
            else
            {
                currentxpath += "/" + treeNode.Text;
            }

            foreach (TreeNode childTreeNode in treeNode.Nodes)
            {
                ApplyConfig(childTreeNode, currentxpath, xmlDocument, proxyAssembly);
            }
        }

        public static void ApplyDefaultConfig(TreeNode treeNode)
        {
            if (treeNode.Tag != null)
            {
                string nodeType = treeNode.Tag.GetType().ToString();
                if (nodeType == "IBS.Utilities.ASMWSTester.ClassProperty")
                {
                    ClassProperty classProperty = treeNode.Tag as ClassProperty;

                    if (classProperty != null)
                    {
                        if (classProperty.Name == "AuthenticationHeaderValue")
                        {
                            classProperty.IsNull = false;
                        }
                    }
                }

                else if (nodeType == "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty")
                {
                    if (((NullablePrimitiveProperty) treeNode.Tag).Name == "Dsn")
                    {
                        if (Configuration.MasterConfig.AuthenticationHeaderSettings.Dsn != "")
                        {
                            ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                Configuration.MasterConfig.AuthenticationHeaderSettings.Dsn;
                        }
                    }
                    else if (((NullablePrimitiveProperty) treeNode.Tag).Name == "Proof")
                    {
                        if (Configuration.MasterConfig.AuthenticationHeaderSettings.Proof != "")
                        {
                            ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                Configuration.MasterConfig.AuthenticationHeaderSettings.Proof;
                        }
                    }
                    else if (((NullablePrimitiveProperty) treeNode.Tag).Name == "UserName")
                    {
                        if (Configuration.MasterConfig.AuthenticationHeaderSettings.UserName != "")
                        {
                            ((NullablePrimitiveProperty) treeNode.Tag).Value =
                                Configuration.MasterConfig.AuthenticationHeaderSettings.UserName;
                        }
                    }
                }
                // else if (nodeType == "PrimitiveProperty")
                //{

                //}
            }


            foreach (TreeNode childTreeNode in treeNode.Nodes)
            {
                ApplyDefaultConfig(childTreeNode);
            }
        }

        public static void ReadOutput(TreeNode treeNode, XmlNode xmlNode, ref XmlDocument xmlDocument)
        {
            XmlElement node1 = null;

            if (treeNode.Tag != null)
            {
                string nodeType = treeNode.Tag.GetType().ToString();

                switch (nodeType)
                {
                    case "IBS.Utilities.ASMWSTester.ClassProperty":
                        node1 = xmlDocument.CreateElement(((ClassProperty) treeNode.Tag).Name);

                        if (((ClassProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        if (xmlNode != null)
                        {
                            xmlNode.AppendChild(node1);
                        }
                        else
                        {
                            xmlDocument.AppendChild(node1);
                        }

                        break;
                    case "IBS.Utilities.ASMWSTester.PrimitiveProperty":
                        node1 = xmlDocument.CreateElement(((PrimitiveProperty) treeNode.Tag).Name);

                        node1.SetAttribute("value", ((PrimitiveProperty) treeNode.Tag).ReadChildren().ToString());
                        node1.SetAttribute("type", ((PrimitiveProperty) treeNode.Tag).Type.ToString());

                        xmlNode.AppendChild(node1);
                        break;
                    case "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty":
                        node1 = xmlDocument.CreateElement(((NullablePrimitiveProperty) treeNode.Tag).Name);

                        if (((NullablePrimitiveProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        if (((NullablePrimitiveProperty) treeNode.Tag).ReadChildren() != null)
                        {
                            node1.SetAttribute("value",
                                               ((NullablePrimitiveProperty) treeNode.Tag).ReadChildren().ToString());
                        }
                        node1.SetAttribute("type", ((NullablePrimitiveProperty) treeNode.Tag).Type.ToString());

                        xmlNode.AppendChild(node1);
                        break;
                    case "IBS.Utilities.ASMWSTester.ArrayProperty":

                        node1 = xmlDocument.CreateElement(((ArrayProperty) treeNode.Tag).Name);

                        if (((ArrayProperty) treeNode.Tag).ReadChildren() == null)
                        {
                            node1.SetAttribute("isNull", "true");
                        }

                        node1.SetAttribute("length", ((ArrayProperty) treeNode.Tag).Length.ToString());

                        if (xmlNode != null)
                        {
                            xmlNode.AppendChild(node1);
                        }
                        else
                        {
                            xmlDocument.AppendChild(node1);
                        }

                        break;
                    default:
                        throw new NotSupportedException("Not Supported Property:" + nodeType);
                }
            }
            else
            {
                node1 = xmlDocument.CreateElement(treeNode.Text);

                if (treeNode.Text != null)
                {
                    xmlNode.AppendChild(node1);
                }
            }

            foreach (TreeNode childnode in treeNode.Nodes)
            {
                ReadOutput(childnode, node1, ref xmlDocument);
            }
        }

        public static string FormatXmlDocumentToString(XmlNode xmlDocument1)
        {
            //will hold formatted xml
            StringBuilder sb = new StringBuilder();

            //pumps the formatted xml into the StringBuilder above
            StringWriter sw = new StringWriter(sb);

            //does the formatting
            XmlTextWriter xtw = null;

            try
            {
                //point the xtw at the StringWriter
                xtw = new XmlTextWriter(sw);

                //we want the output formatted
                xtw.Formatting = Formatting.Indented;

                //get the dom to dump its contents into the xtw 
                xmlDocument1.WriteTo(xtw);
            }
            finally
            {
                //clean up even if error
                if (xtw != null)
                    xtw.Close();
            }

            return sb.ToString();
        }
    }
}