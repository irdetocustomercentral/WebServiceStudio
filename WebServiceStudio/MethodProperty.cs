using System;
using System.Collections;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
{
    internal class MethodProperty : TreeNodeProperty
    {
        public MethodProperty(ProxyProperty proxyProperty, MethodInfo method)
            : base(new Type[] {method.ReturnType}, method.Name)
        {
            this.proxyProperty = proxyProperty;
            this.method = method;
            isIn = true;
        }

        public MethodProperty(ProxyProperty proxyProperty, MethodInfo method, object result, object[] paramValues)
            : base(new Type[] {method.ReturnType}, method.Name)
        {
            this.proxyProperty = proxyProperty;
            this.method = method;
            isIn = false;
            this.result = result;
            this.paramValues = paramValues;
        }

        private void AddBody()
        {
            TreeNode node1 = base.TreeNode.Nodes.Add("Body");
            if (!isIn && (method.ReturnType != typeof (void)))
            {
                Type type1 = (result != null) ? result.GetType() : method.ReturnType;
                CreateTreeNodeProperty(new Type[] {type1}, "result", result).RecreateSubtree(node1);
            }
            ParameterInfo[] infoArray1 = method.GetParameters();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                if ((!isIn && (infoArray1[num1].IsOut || infoArray1[num1].ParameterType.IsByRef)) ||
                    (isIn && !infoArray1[num1].IsOut))
                {
                    Type type2 = infoArray1[num1].ParameterType;
                    if (type2.IsByRef)
                    {
                        type2 = type2.GetElementType();
                    }
                    object obj1 = (paramValues != null) ? paramValues[num1] : (isIn ? CreateNewInstance(type2) : null);
                    CreateTreeNodeProperty(base.GetIncludedTypes(type2), infoArray1[num1].Name, obj1).RecreateSubtree(
                        node1);
                }
            }
            node1.ExpandAll();
        }

        private void AddHeaders()
        {
            TreeNode node1 = base.TreeNode.Nodes.Add("Headers");
            FieldInfo[] infoArray1 = GetSoapHeaders(method, isIn);
            HttpWebClientProtocol protocol1 = proxyProperty.GetProxy();
            foreach (FieldInfo info1 in infoArray1)
            {
                object obj1 = (protocol1 != null) ? info1.GetValue(protocol1) : null;
                CreateTreeNodeProperty(base.GetIncludedTypes(info1.FieldType), info1.Name, obj1).RecreateSubtree(node1);
            }
            node1.ExpandAll();
        }

        protected override void CreateChildren()
        {
            AddHeaders();
            AddBody();
        }

        protected override MethodInfo GetCurrentMethod()
        {
            return method;
        }

        protected override object GetCurrentProxy()
        {
            return proxyProperty.GetProxy();
        }

        public MethodInfo GetMethod()
        {
            return method;
        }

        public ProxyProperty GetProxyProperty()
        {
            return proxyProperty;
        }

        public static FieldInfo[] GetSoapHeaders(MethodInfo method, bool isIn)
        {
            Type type1 = method.DeclaringType;
            SoapHeaderAttribute[] attributeArray1 =
                (SoapHeaderAttribute[]) method.GetCustomAttributes(typeof (SoapHeaderAttribute), true);
            ArrayList list1 = new ArrayList();
            for (int num1 = 0; num1 < attributeArray1.Length; num1++)
            {
                SoapHeaderAttribute attribute1 = attributeArray1[num1];
                if (((attribute1.Direction == SoapHeaderDirection.InOut) ||
                     (isIn && (attribute1.Direction == SoapHeaderDirection.In))) ||
                    (!isIn && (attribute1.Direction == SoapHeaderDirection.Out)))
                {
                    FieldInfo info1 = type1.GetField(attribute1.MemberName);
                    list1.Add(info1);
                }
            }
            return (FieldInfo[]) list1.ToArray(typeof (FieldInfo));
        }

        protected override bool IsInput()
        {
            return isIn;
        }

        private void ReadBody()
        {
            TreeNode node1 = base.TreeNode.Nodes[1];
            ParameterInfo[] infoArray1 = method.GetParameters();
            paramValues = new object[infoArray1.Length];
            int num1 = 0;
            int num2 = 0;
            while (num1 < paramValues.Length)
            {
                ParameterInfo info1 = infoArray1[num1];
                if (!info1.IsOut)
                {
                    TreeNode node2 = node1.Nodes[num2++];
                    TreeNodeProperty property1 = node2.Tag as TreeNodeProperty;
                    if (property1 != null)
                    {
                        paramValues[num1] = property1.ReadChildren();
                    }
                }
                num1++;
            }
        }

        public override object ReadChildren()
        {
            ReadHeaders();
            ReadBody();
            return paramValues;
        }

        private void ReadHeaders()
        {
            TreeNode node1 = base.TreeNode.Nodes[0];
            Type type1 = method.DeclaringType;
            HttpWebClientProtocol protocol1 = proxyProperty.GetProxy();
            foreach (TreeNode node2 in node1.Nodes)
            {
                ClassProperty property1 = node2.Tag as ClassProperty;
                if (property1 != null)
                {
                    type1.GetField(property1.Name).SetValue(protocol1, property1.ReadChildren());
                }
            }
        }

        public override string ToString()
        {
            return base.Name;
        }


        private bool isIn;
        private MethodInfo method;
        private object[] paramValues;
        private ProxyProperty proxyProperty;
        private object result;
    }
}