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
