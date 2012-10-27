
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.Xml;

namespace WebServiceStudio
{
    internal class XmlAttributeProperty : ClassProperty
    {
        static XmlAttributeProperty()
        {
            stringType = new Type[] {typeof (string)};
        }

        public XmlAttributeProperty(Type[] possibleTypes, string name, object val) : base(possibleTypes, name, val)
        {
        }

        protected override void CreateChildren()
        {
            base.TreeNode.Nodes.Clear();
            if (base.InternalValue != null)
            {
                CreateTreeNodeProperty(stringType, "Name", xmlAttribute.Name).RecreateSubtree(base.TreeNode);
                CreateTreeNodeProperty(stringType, "NamespaceURI", xmlAttribute.NamespaceURI).RecreateSubtree(
                    base.TreeNode);
                CreateTreeNodeProperty(stringType, "Value", xmlAttribute.Value).RecreateSubtree(base.TreeNode);
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
                return xmlAttribute.OwnerDocument;
            }
            return property2.GetXmlDocument();
        }

        public override object ReadChildren()
        {
            if (base.InternalValue == null)
            {
                return null;
            }
            string text1 = ((TreeNodeProperty) base.TreeNode.Nodes[0].Tag).ReadChildren().ToString();
            string text2 = ((TreeNodeProperty) base.TreeNode.Nodes[1].Tag).ReadChildren().ToString();
            string text3 = ((TreeNodeProperty) base.TreeNode.Nodes[2].Tag).ReadChildren().ToString();
            xmlAttribute = GetXmlDocument().CreateAttribute(text1, text2);
            xmlAttribute.Value = text3;
            return xmlAttribute;
        }


        private XmlAttribute xmlAttribute
        {
            get { return (base.InternalValue as XmlAttribute); }
            set { base.InternalValue = value; }
        }


        private static Type[] stringType;
    }
}
