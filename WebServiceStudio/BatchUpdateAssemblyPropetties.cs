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
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class BatchUpdateAssemblyPropetties
    {
        public BatchUpdateAssemblyPropetties()
        {
            updateModuleNames = new ArrayList();
            updateVersions = new ArrayList();
        }

        public string[] GetProxyBaseTypeList()
        {
            return Configuration.MasterConfig.GetProxyBaseTypes();
        }

        public override string ToString()
        {
            return "";
        }

        [XmlArrayItem("modulename", typeof (string)), Browsable(true)]
        public string[] UpdateModuleNames
        {
            get { return (updateModuleNames.ToArray(typeof (string)) as string[]); }
            set
            {
                updateModuleNames.Clear();
                if (value != null)
                {
                    updateModuleNames.AddRange(value);
                }
            }
        }

        public void AddUpdateModuleName(string moduleName)
        {
            updateModuleNames.Add(moduleName);
            Configuration.SaveMasterConfig();
        }


        private ArrayList updateModuleNames;

        [XmlArrayItem("version", typeof(string)), Browsable(true)]
        public string[] UpdateVersions
        {
            get { return (updateVersions.ToArray(typeof(string)) as string[]); }
            set
            {
                updateVersions.Clear();
                if (value != null)
                {
                    updateVersions.AddRange(value);
                }
            }
        }

        private ArrayList updateVersions;

        [XmlAttribute]
        public bool AutoUpdate
        {
            get { return autoUpdate; }
            set { autoUpdate = value; }
        }

        private bool autoUpdate;
    }
}
