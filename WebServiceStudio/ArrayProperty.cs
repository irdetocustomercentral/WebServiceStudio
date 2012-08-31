<<<<<<< HEAD
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
	




=======
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
>>>>>>> SandBox
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IBS.Utilities.ASMWSTester
{
    internal class ArrayProperty : ClassProperty
    {
        public ArrayProperty(Type[] possibleTypes, string name, Array val) : base(possibleTypes, name, val)
        {
        }

        protected override void CreateChildren()
        {
            base.TreeNode.Nodes.Clear();
            if (OkayToCreateChildren())
            {
                Type type1 = Type.GetElementType();
                int num1 = Length;
                for (int num2 = 0; num2 < num1; num2++)
                {
                    object obj1 = ArrayValue.GetValue(num2);
                    if ((obj1 == null) && IsInput())
                    {
                        obj1 = CreateNewInstance(type1);
                    }
                    CreateTreeNodeProperty(base.GetIncludedTypes(type1), base.Name + "_" + num2.ToString(), obj1).
                        RecreateSubtree(base.TreeNode);
                }
            }
        }

        public override object ReadChildren()
        {
            Array array1 = ArrayValue;
            if (array1 == null)
            {
                return null;
            }
            int num1 = 0;
            for (int num2 = 0; num2 < array1.Length; num2++)
            {
                TreeNode node1 = base.TreeNode.Nodes[num1++];
                TreeNodeProperty property1 = node1.Tag as TreeNodeProperty;
                if (property1 != null)
                {
                    array1.SetValue(property1.ReadChildren(), num2);
                }
            }
            return array1;
        }


        private Array ArrayValue
        {
            get { return (base.InternalValue as Array); }
            set { base.InternalValue = value; }
        }

        [RefreshProperties(RefreshProperties.All)]
        public virtual int Length
        {
            get { return ((ArrayValue != null) ? ArrayValue.Length : 0); }
            set
            {
                int num1 = Length;
                int num2 = value;
                Array array1 = Array.CreateInstance(Type.GetElementType(), num2);
                if (ArrayValue != null)
                {
                    Array.Copy(ArrayValue, array1, Math.Min(num2, num1));
                }
                ArrayValue = array1;
                base.TreeNode.Text = ToString();
                CreateChildren();
            }
        }
    }
}
