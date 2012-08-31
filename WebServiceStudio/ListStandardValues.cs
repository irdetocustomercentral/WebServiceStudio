	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace IBS.Utilities.ASMWSTester
{
    internal class ListStandardValues : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof (string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Array array1 = GetPossibleValues(context);
                if (array1 != null)
                {
                    foreach (object obj1 in array1)
                    {
                        if (obj1.ToString() == value.ToString())
                        {
                            return obj1;
                        }
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        protected virtual Array GetPossibleValues(ITypeDescriptorContext context)
        {
            MethodInfo info1 = context.Instance.GetType().GetMethod("Get" + context.PropertyDescriptor.Name + "List");
            if (info1 == null)
            {
                return null;
            }
            return (info1.Invoke(context.Instance, null) as Array);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            Array array1 = GetPossibleValues(context);
            return new StandardValuesCollection((array1 != null) ? array1 : new object[0]);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
