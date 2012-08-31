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
using System.Reflection;
using System.Xml;

namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public abstract class XmlConfigNodeBase
    {
        private XmlNode xmlNode;

        protected string nodeName = String.Empty;

        protected object locker = new object();

        //public XmlConfigNodeBase()
        //{
        //    //xmlNode = BatchRunCongifFileHelper.XmlDocumen.CreateElement(nodeName);
        //}

        //protected bool existNodeData=false;

        public XmlNode ToXML()
        {
            //if(existNodeData)
            //{
            //    return xmlNode;
            //}

            //xmlNode.RemoveAll();
            xmlNode = BatchRunCongifFileHelper.XmlDocumen.CreateElement(nodeName);

            lock (locker)
            {
                PropertyInfo[] propertyInfos = GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.PropertyType.IsSubclassOf(typeof (XmlConfigNodeBase)))
                    {
                        xmlNode.AppendChild(propertyInfo.PropertyType.InvokeMember("ToXML", BindingFlags.Default |
                                                                                            BindingFlags.InvokeMethod,
                                                                                   null,
                                                                                   this, null) as XmlNode);
                    }
                    else if (propertyInfo.PropertyType.IsValueType || typeof (String).Equals(propertyInfo.PropertyType))
                    {
                        XmlAttribute xmlAttribute =
                            BatchRunCongifFileHelper.XmlDocumen.CreateAttribute(propertyInfo.Name.ToLower());
                        xmlAttribute.Value = GetPropertyValue(this, propertyInfo.Name).ToString();
                        xmlNode.Attributes.Append(xmlAttribute);
                    }
                    else if (propertyInfo.PropertyType.GetInterface("IEnumerable") != null)
                    {
                        object propertyValue = GetPropertyValue(this, propertyInfo.Name);

                        XmlNode xmlNode1 =
                            BatchRunCongifFileHelper.XmlDocumen.CreateElement(propertyInfo.Name.ToLower());

                        xmlNode.AppendChild(xmlNode1);

                        if (propertyValue != null)
                        {
                            IEnumerator ie = (propertyValue as IEnumerable).GetEnumerator();
                            while (ie.MoveNext())
                            {
                                XmlNode currentNode =
                                    ie.Current.GetType().InvokeMember("ToXML",
                                                                      BindingFlags.Default | BindingFlags.InvokeMethod,
                                                                      null,
                                                                      ie.Current, null) as XmlNode;
                                xmlNode1.AppendChild(currentNode);
                            }
                        }
                    }
                    //else
                    //{
                    //    throw new Exception("");
                    //}
                }

                //existNodeData = true;

                return xmlNode;
            }
        }

        public object GetPropertyValue(object ClassInstance, string PropertyName)
        {
            Type myType = ClassInstance.GetType();
            PropertyInfo myPropertyInfo = myType.GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.Public);
            return myPropertyInfo.GetValue(ClassInstance, null);
        }
    }
}
