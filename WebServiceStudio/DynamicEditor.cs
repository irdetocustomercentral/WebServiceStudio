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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;

namespace IBS.Utilities.ASMWSTester
{
    public class DynamicEditor : UITypeEditor
    {
        static DynamicEditor()
        {
            editorTable = null;
            typeNotFoundMessage =
                "ProxyPropertiesType {0} specified in IBS.Utilities.ASMWSTester.exe.options is not found";
            editorTable = new Hashtable();
            editorTable[typeof (object)] = Activator.CreateInstance(typeof (UITypeEditor));
            editorTable[typeof (DataSet)] = Activator.CreateInstance(typeof (DataSetEditor));
            editorTable[typeof (DateTime)] = Activator.CreateInstance(typeof (DateTimeEditor));
            foreach (CustomHandler handler1 in Configuration.MasterConfig.DataEditors)
            {
                Type type1 = Type.GetType(handler1.TypeName);
                if (type1 == null)
                {
                    MainForm.ShowMessage(typeof (DynamicEditor), MessageType.Warning,
                                         string.Format(typeNotFoundMessage, handler1.TypeName));
                }
                else
                {
                    Type type2 = Type.GetType(handler1.Handler);
                    if (type2 == null)
                    {
                        MainForm.ShowMessage(typeof (DynamicEditor), MessageType.Warning,
                                             string.Format(typeNotFoundMessage, handler1.Handler));
                    }
                    else
                    {
                        editorTable[type1] = Activator.CreateInstance(type2);
                    }
                }
            }
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Type type1 = null;
            if (value == null)
            {
                TreeNodeProperty property1 = context.Instance as TreeNodeProperty;
                if (property1 != null)
                {
                    type1 = property1.Type;
                }
            }
            else
            {
                type1 = value.GetType();
            }
            if (type1 != null)
            {
                return GetEditor(type1).EditValue(context, provider, value);
            }
            return base.EditValue(context, provider, value);
        }

        private Type GetContainedType(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                TreeNodeProperty property1 = context.Instance as TreeNodeProperty;
                if (property1 != null)
                {
                    return property1.Type;
                }
            }
            return null;
        }

        public static UITypeEditor GetEditor(Type type)
        {
            object obj1 = null;
            if (type != null)
            {
                if (typeof (DataSet).IsAssignableFrom(type))
                {
                    type = typeof (DataSet);
                }
                obj1 = editorTable[type];
            }
            if (obj1 == null)
            {
                obj1 = editorTable[typeof (object)];
            }
            return (obj1 as UITypeEditor);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return GetEditor(GetContainedType(context)).GetEditStyle(context);
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return base.GetPaintValueSupported(context);
        }

        public static bool IsEditorDefined(Type type)
        {
            if (GetEditor(type).GetType().IsAssignableFrom(typeof (UITypeEditor)))
            {
                return false;
            }
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            base.PaintValue(e);
        }


        private static Hashtable editorTable;
        private static string typeNotFoundMessage;
    }
}
