	//WebServiceStudio Application to test WCF web services.

	//Copyright (C) 2012  Irdeto Customer Care And Billing

	//This program is free software: you can redistribute it and/or modify
	//it under the terms of the GNU General Public License as published by
	//the Free Software Foundation, either version 3 of the License, or
	//(at your option) any later version.

	//This program is distributed in the hope that it will be useful,
	//but WITHOUT ANY WARRANTY; without even the implied warranty of
	//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//GNU General Public License for more details.

	//You should have received a copy of the GNU General Public License
	//along with this program.  If not, see http://www.gnu.org/licenses/

	//Web service Studio has been modifided to understand more complex input types
	//such as Iredeto's Customer Care input type of Base Query Request. 
	




using System;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
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
