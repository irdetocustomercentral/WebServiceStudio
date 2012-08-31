	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace IBS.Utilities.ASMWSTester
{
    internal class NullablePrimitiveProperty : ClassProperty
    {
        public NullablePrimitiveProperty(Type[] possibleTypes, string name, object val) : base(possibleTypes, name, val)
        {
        }

        protected override void CreateChildren()
        {
        }

        public override object ReadChildren()
        {
            return Value;
        }

        public override string ToString()
        {
            string text1 = base.ToString();
            if (Value == null)
            {
                return text1;
            }
            return (text1 + " = " + Value.ToString());
        }


        [RefreshProperties(RefreshProperties.All), Editor(typeof (DynamicEditor), typeof (UITypeEditor)),
         TypeConverter(typeof (DynamicConverter))]
        public object Value
        {
            get { return base.InternalValue; }
            set
            {
                base.InternalValue = value;
                base.TreeNode.Text = ToString();
            }
        }
    }
}
