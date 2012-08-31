
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
{
    internal class ClassProperty : TreeNodeProperty
    {
        public ClassProperty(Type[] possibleTypes, string name, object val) : base(possibleTypes, name)
        {
            isNull = false;
            this.val = val;
            isNull = this.val == null;
        }

        protected override void CreateChildren()
        {
            base.TreeNode.Nodes.Clear();
            if (OkayToCreateChildren())
            {
                foreach (PropertyInfo info1 in Type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object obj1 = info1.GetValue(val, null);
                    if ((obj1 == null) && IsInput())
                    {
                        obj1 = CreateNewInstance(info1.PropertyType);
                    }
                    CreateTreeNodeProperty(base.GetIncludedTypes(info1.PropertyType), info1.Name, obj1).RecreateSubtree(
                        base.TreeNode);
                }
                foreach (FieldInfo info2 in Type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    object obj2 = info2.GetValue(val);
                    if ((obj2 == null) && IsInput())
                    {
                        obj2 = CreateNewInstance(info2.FieldType);
                    }
                    CreateTreeNodeProperty(base.GetIncludedTypes(info2.FieldType), info2.Name, obj2).RecreateSubtree(
                        base.TreeNode);
                }
            }
        }

        protected virtual bool OkayToCreateChildren()
        {
            if (IsInternalType(Type))
            {
                return false;
            }
            if (IsDeepNesting(this))
            {
                InternalValue = null;
            }
            if (InternalValue == null)
            {
                return false;
            }
            return true;
        }

        public override object ReadChildren()
        {
            object obj1 = InternalValue;
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
            FieldInfo[] infoArray2 = Type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo info2 in infoArray2)
            {
                TreeNode node2 = base.TreeNode.Nodes[num1++];
                TreeNodeProperty property2 = node2.Tag as TreeNodeProperty;
                if (property2 != null)
                {
                    info2.SetValue(obj1, property2.ReadChildren(), BindingFlags.Public, null, null);
                }
            }
            return obj1;
        }

        public virtual object ToObject()
        {
            return InternalValue;
        }

        public override string ToString()
        {
            return (base.GetTypeList()[0].Name + " " + base.Name + (IsNull ? " = null" : ""));
        }


        internal object InternalValue
        {
            get { return (isNull ? null : val); }
            set
            {
                val = value;
                isNull = value == null;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public bool IsNull
        {
            get { return isNull; }
            set
            {
                if (isNull != value)
                {
                    if (!value)
                    {
                        if (val == null)
                        {
                            val = CreateNewInstance(Type);
                        }
                        if (val == null)
                        {
                            MessageBox.Show("Not able to create an instance of " + Type.FullName);
                            value = true;
                        }
                    }
                    else
                    {
                        ReadChildren();
                    }
                    isNull = value;
                    CreateChildren();
                    base.TreeNode.Text = ToString();
                }
            }
        }

        public override Type Type
        {
            get { return ((InternalValue != null) ? InternalValue.GetType() : base.Type); }
            set
            {
                try
                {
                    if (Type != value)
                    {
                        InternalValue = CreateNewInstance(value);
                    }
                }
                catch
                {
                    InternalValue = null;
                }
            }
        }


        private bool isNull;
        private object val;
    }
}
