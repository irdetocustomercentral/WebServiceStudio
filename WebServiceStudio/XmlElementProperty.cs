using System;
using System.Collections;
using System.Xml;

namespace IBS.Utilities.ASMWSTester
{
    internal class XmlElementProperty : ClassProperty
    {
        static XmlElementProperty()
        {
            stringType = new Type[] {typeof (string)};
            attrArrayType = new Type[] {typeof (XmlAttribute[])};
            elemArrayType = new Type[] {typeof (XmlElement[])};
        }

        public XmlElementProperty(Type[] possibleTypes, string name, object val) : base(possibleTypes, name, val)
        {
        }

        protected override void CreateChildren()
        {
            base.TreeNode.Nodes.Clear();
            if (base.InternalValue != null)
            {
                CreateTreeNodeProperty(stringType, "Name", xmlElement.Name).RecreateSubtree(base.TreeNode);
                CreateTreeNodeProperty(stringType, "NamespaceURI", xmlElement.NamespaceURI).RecreateSubtree(
                    base.TreeNode);
                CreateTreeNodeProperty(stringType, "TextValue", xmlElement.InnerText).RecreateSubtree(base.TreeNode);
                ArrayList list1 = new ArrayList();
                ArrayList list2 = new ArrayList();
                if (xmlElement != null)
                {
                    for (XmlNode node1 = xmlElement.FirstChild; node1 != null; node1 = node1.NextSibling)
                    {
                        if (node1.NodeType == XmlNodeType.Element)
                        {
                            list2.Add(node1);
                        }
                    }
                    foreach (XmlAttribute attribute1 in xmlElement.Attributes)
                    {
                        if ((attribute1.Name != "xmlns") && !attribute1.Name.StartsWith("xmlns:"))
                        {
                            list1.Add(attribute1);
                        }
                    }
                }
                XmlAttribute[] attributeArray1 = ((list1.Count == 0) && !IsInput())
                                                     ? null
                                                     : (list1.ToArray(typeof (XmlAttribute)) as XmlAttribute[]);
                XmlElement[] elementArray1 = ((list2.Count == 0) && !IsInput())
                                                 ? null
                                                 : (list2.ToArray(typeof (XmlElement)) as XmlElement[]);
                CreateTreeNodeProperty(attrArrayType, "Attributes", attributeArray1).RecreateSubtree(base.TreeNode);
                CreateTreeNodeProperty(elemArrayType, "SubElements", elementArray1).RecreateSubtree(base.TreeNode);
            }
        }

        public XmlDocument GetXmlDocument()
        {
            ArrayProperty property1 = base.GetParent() as ArrayProperty;
            XmlElementProperty property2 = null;
            if (property1 != null)
            {
                property2 = property1.GetParent() as XmlElementProperty;
            }
            if (property2 == null)
            {
                return xmlElement.OwnerDocument;
            }
            return property2.GetXmlDocument();
        }

        public override object ReadChildren()
        {
            XmlElement element3;
            if (base.InternalValue == null)
            {
                return null;
            }
            string text1 = ((TreeNodeProperty) base.TreeNode.Nodes[0].Tag).ReadChildren().ToString();
            string text2 = ((TreeNodeProperty) base.TreeNode.Nodes[1].Tag).ReadChildren().ToString();
            string text3 = ((TreeNodeProperty) base.TreeNode.Nodes[2].Tag).ReadChildren().ToString();
            XmlAttribute[] attributeArray1 =
                (XmlAttribute[]) ((TreeNodeProperty) base.TreeNode.Nodes[3].Tag).ReadChildren();
            XmlElement[] elementArray1 = (XmlElement[]) ((TreeNodeProperty) base.TreeNode.Nodes[4].Tag).ReadChildren();
            XmlElement element1 = GetXmlDocument().CreateElement(text1, text2);
            if (attributeArray1 != null)
            {
                foreach (XmlAttribute attribute1 in attributeArray1)
                {
                    element1.SetAttributeNode(attribute1);
                }
            }
            element1.InnerText = text3;
            if (elementArray1 != null)
            {
                foreach (XmlElement element2 in elementArray1)
                {
                    element1.AppendChild(element2);
                }
            }
            xmlElement = element3 = element1;
            return element3;
        }


        private XmlElement xmlElement
        {
            get { return (base.InternalValue as XmlElement); }
            set { base.InternalValue = value; }
        }


        private static Type[] attrArrayType;
        private static Type[] elemArrayType;
        private static Type[] stringType;
    }
}