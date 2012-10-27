
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace WebServiceStudio
{
    internal class PrimitiveProperty : TreeNodeProperty
    {
        public PrimitiveProperty(string name, object val) : base(new Type[] {val.GetType()}, name)
        {
            this.val = val;
        }

        public override object ReadChildren()
        {
            return Value;
        }

        public override string ToString()
        {
            return string.Concat(new object[] {Type.Name, " ", base.Name, " = ", Value});
        }


        [Editor(typeof (DynamicEditor), typeof (UITypeEditor)), TypeConverter(typeof (DynamicConverter))]
        public object Value
        {
            get { return val; }
            set
            {
                val = value;
                base.TreeNode.Text = ToString();
            }
        }


        private object val;
    }
}
