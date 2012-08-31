	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    internal class TreeNodeProperty
    {
        static TreeNodeProperty()
        {
            includedTypesLookup = new Hashtable();
            systemTypes = new Type[]
                {
                    typeof (bool), typeof (byte), typeof (byte[]), typeof (sbyte), typeof (short), typeof (int),
                    typeof (long), typeof (float), typeof (double), typeof (string), typeof (string[]),
                    typeof (DateTime), typeof (TimeSpan), typeof (XmlElement), typeof (XmlAttribute), typeof (XmlNode[])
                    ,
                    typeof (object[]), typeof (Decimal)
                };
        }

        public TreeNodeProperty(Type[] types, string name)
        {
            this.types = types;
            Name = name;
        }

        public void AddToTypeList(object o)
        {
            Type type1 = o as Type;
            Type[] typeArray1 = new Type[types.Length + 1];
            Array.Copy(types, typeArray1, types.Length);
            typeArray1[types.Length] = type1;
            types = typeArray1;
        }

        private void AddTypeToList(Type[] includedTypes, ArrayList list)
        {
            Type type1 = list[0] as Type;
            foreach (Type type2 in includedTypes)
            {
                if (type1.IsAssignableFrom(type2) && !list.Contains(type2))
                {
                    list.Add(type2);
                }
            }
        }

        public static void ClearIncludedTypes()
        {
            includedTypesLookup.Clear();
        }

        protected virtual void CreateChildren()
        {
        }

        protected static object CreateNewInstance(Type type)
        {
            try
            {
                if (type.IsArray)
                {
                    return Array.CreateInstance(type.GetElementType(), 1);
                }
                if (type == typeof (string))
                {
                    return "";
                }
                if (type == typeof (Guid))
                {
                    return Guid.NewGuid();
                }
                if (type == typeof (XmlElement))
                {
                    XmlDocument document1 = new XmlDocument();
                    return document1.CreateElement("MyElement");
                }
                if (type == typeof (XmlAttribute))
                {
                    XmlDocument document2 = new XmlDocument();
                    return document2.CreateAttribute("MyAttribute");
                }
                return Activator.CreateInstance(type);
            }
            catch
            {
            }
            return null;
        }

        public static TreeNodeProperty CreateTreeNodeProperty(TreeNodeProperty tnp)
        {
            if (tnp is ClassProperty)
            {
                ClassProperty property1 = tnp as ClassProperty;
                return CreateTreeNodeProperty(property1.types, property1.Name, property1.InternalValue);
            }
            if (tnp is PrimitiveProperty)
            {
                PrimitiveProperty property2 = tnp as PrimitiveProperty;
                return CreateTreeNodeProperty(property2.types, property2.Name, property2.Value);
            }
            return CreateTreeNodeProperty(tnp.types, tnp.Name, null);
        }

        public static TreeNodeProperty CreateTreeNodeProperty(TreeNodeProperty tnp, object val)
        {
            return CreateTreeNodeProperty(tnp.types, tnp.Name, val);
        }

        public static TreeNodeProperty CreateTreeNodeProperty(Type[] possibleTypes, string name, object val)
        {
            Type type1 = (val == null) ? possibleTypes[0] : val.GetType();
            if (type1.IsByRef)
            {
                type1 = type1.GetElementType();
            }
            if (IsPrimitiveType(possibleTypes[0]))
            {
                if (val == null)
                {
                    val = CreateNewInstance(type1);
                }
                return new PrimitiveProperty(name, val);
            }
            if (IsNullablePrimitiveType(type1) || IsPrimitiveType(type1))
            {
                return new NullablePrimitiveProperty(possibleTypes, name, val);
            }
            if (typeof (XmlElement).IsAssignableFrom(type1))
            {
                return new XmlElementProperty(possibleTypes, name, val);
            }
            if (typeof (XmlAttribute).IsAssignableFrom(type1))
            {
                return new XmlAttributeProperty(possibleTypes, name, val);
            }
            if (type1.IsArray)
            {
                return new ArrayProperty(possibleTypes, name, val as Array);
            }
            return new ClassProperty(possibleTypes, name, val);
        }

        private static Type[] GetAllIncludedTypes(Type webService)
        {
            Type[] typeArray1 = includedTypesLookup[webService] as Type[];
            if (typeArray1 == null)
            {
                ArrayList list1 = new ArrayList();
                SoapIncludeAttribute[] attributeArray1 =
                    webService.GetCustomAttributes(typeof (SoapIncludeAttribute), true) as SoapIncludeAttribute[];
                foreach (SoapIncludeAttribute attribute1 in attributeArray1)
                {
                    list1.Add(attribute1.Type);
                }
                XmlIncludeAttribute[] attributeArray2 =
                    webService.GetCustomAttributes(typeof (XmlIncludeAttribute), true) as XmlIncludeAttribute[];
                foreach (XmlIncludeAttribute attribute2 in attributeArray2)
                {
                    list1.Add(attribute2.Type);
                }
                foreach (Type type1 in systemTypes)
                {
                    list1.Add(type1);
                }
                typeArray1 = (Type[]) list1.ToArray(typeof (Type));
                includedTypesLookup[webService] = typeArray1;
            }
            return typeArray1;
        }

        protected virtual MethodInfo GetCurrentMethod()
        {
            TreeNodeProperty property1 = GetParent();
            if (property1 == null)
            {
                return null;
            }
            return property1.GetCurrentMethod();
        }

        protected virtual object GetCurrentProxy()
        {
            TreeNodeProperty property1 = GetParent();
            if (property1 == null)
            {
                return null;
            }
            return property1.GetCurrentProxy();
        }

        protected Type[] GetIncludedTypes(Type type)
        {
            ArrayList list1 = new ArrayList();
            list1.Add(type);
            if (type.IsByRef)
            {
                type = type.GetElementType();
            }
            MethodInfo info1 = GetCurrentMethod();
            if (info1 != null)
            {
                AddTypeToList(GetAllIncludedTypes(info1.DeclaringType), list1);
            }
            AddTypeToList(GetAllIncludedTypes(type), list1);
            return (Type[]) list1.ToArray(typeof (Type));
        }

        public TreeNodeProperty GetParent()
        {
            if (TreeNode != null)
            {
                TreeNode node1 = TreeNode;
                while (node1.Parent != null)
                {
                    node1 = node1.Parent;
                    TreeNodeProperty property1 = node1.Tag as TreeNodeProperty;
                    if (property1 != null)
                    {
                        return property1;
                    }
                }
            }
            return null;
        }

        public Type[] GetTypeList()
        {
            return types;
        }

        protected static bool IsDeepNesting(TreeNodeProperty tnp)
        {
            if (tnp != null)
            {
                int num1 = 0;
                while ((tnp = tnp.GetParent()) != null)
                {
                    num1++;
                    if (num1 > 12)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected virtual bool IsInput()
        {
            TreeNodeProperty property1 = GetParent();
            if (property1 == null)
            {
                return false;
            }
            return property1.IsInput();
        }

        protected static bool IsInternalType(Type type)
        {
            return typeof (Type).IsAssignableFrom(type);
        }

        private static bool IsNullablePrimitiveType(Type type)
        {
            if (((typeof (string) == type) || (typeof (Guid) == type)) || typeof (DataSet).IsAssignableFrom(type))
            {
                return true;
            }
            if (DynamicEditor.IsEditorDefined(type) || DynamicConverter.IsConverterDefined(type))
            {
                return true;
            }
            return false;
        }

        protected static bool IsPrimitiveType(Type type)
        {
            return
                (((type.IsEnum || type.IsPrimitive) || ((type == typeof (DateTime)) || (type == typeof (TimeSpan))))); //||
                 //(type == typeof (decimal)));
        }

        public static bool IsWebMethod(MethodInfo method)
        {
            object[] objArray1 = method.GetCustomAttributes(typeof (SoapRpcMethodAttribute), true);
            if ((objArray1 != null) && (objArray1.Length > 0))
            {
                return true;
            }
            objArray1 = method.GetCustomAttributes(typeof (SoapDocumentMethodAttribute), true);
            if ((objArray1 != null) && (objArray1.Length > 0))
            {
                return true;
            }
            objArray1 = method.GetCustomAttributes(typeof (HttpMethodAttribute), true);
            if ((objArray1 != null) && (objArray1.Length > 0))
            {
                return true;
            }
            return false;
        }

        public static bool IsWebService(Type type)
        {
            return typeof (HttpWebClientProtocol).IsAssignableFrom(type);
        }

        public virtual object ReadChildren()
        {
            return null;
        }

        public void RecreateSubtree(TreeNode parentNode)
        {
            int num1 = -1;
            if (TreeNode != null)
            {
                if (parentNode == null)
                {
                    parentNode = TreeNode.Parent;
                }
                if (TreeNode.Parent == parentNode)
                {
                    num1 = TreeNode.Index;
                }
                TreeNode.Remove();
            }
            TreeNode = new TreeNode(ToString());
            TreeNode.Tag = this;
            if (parentNode != null)
            {
                if (num1 < 0)
                {
                    parentNode.Nodes.Add(TreeNode);
                }
                else
                {
                    parentNode.Nodes.Insert(num1, TreeNode);
                }
            }
            CreateChildren();
        }

        public override string ToString()
        {
            return Name;
        }


        [TypeConverter(typeof (ListStandardValues))]
        public virtual Type Type
        {
            get { return types[0]; }
            set { }
        }


        private static Hashtable includedTypesLookup;
        public string Name;
        private static Type[] systemTypes;
        public TreeNode TreeNode;
        private Type[] types;
    }
}
