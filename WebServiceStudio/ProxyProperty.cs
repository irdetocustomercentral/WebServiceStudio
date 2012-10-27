
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace WebServiceStudio
{
    internal class ProxyProperty : TreeNodeProperty
    {
        public ProxyProperty(HttpWebClientProtocol proxy) : base(new Type[] {typeof (ProxyProperties)}, "Proxy")
        {
            this.proxy = proxy;
            proxyProperties = new ProxyProperties(proxy);
        }

        protected override void CreateChildren()
        {
            base.TreeNode.Nodes.Clear();
            foreach (PropertyInfo info1 in Type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object obj1 = info1.GetValue(proxyProperties, null);
                CreateTreeNodeProperty(base.GetIncludedTypes(info1.PropertyType), info1.Name, obj1).RecreateSubtree(
                    base.TreeNode);
            }
        }

        public HttpWebClientProtocol GetProxy()
        {
            ((ProxyProperties) ReadChildren()).UpdateProxy(proxy);
            return proxy;
        }

        public override object ReadChildren()
        {
            object obj1 = proxyProperties;
            if (obj1 == null)
            {
                return null;
            }
            int num1 = 0;
            PropertyInfo[] infoArray1 = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info1 in infoArray1)
            {
                TreeNode node1 = base.TreeNode.Nodes[num1++];
                TreeNodeProperty property1 = node1.Tag as TreeNodeProperty;
                if (property1 != null)
                {
                    info1.SetValue(obj1, property1.ReadChildren(), null);
                }
            }
            return obj1;
        }


        private HttpWebClientProtocol proxy;
        private ProxyProperties proxyProperties;
    }
}
