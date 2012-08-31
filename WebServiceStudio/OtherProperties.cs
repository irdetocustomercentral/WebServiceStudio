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
using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class OtherProperties
    {
        public string[] GetProxyBaseTypeList()
        {
            return Configuration.MasterConfig.GetProxyBaseTypes();
        }

        public override string ToString()
        {
            return "";
        }


        [XmlAttribute]
        public string TemplateFileName
        {
            get { return templateFileName; }
            set { templateFileName = value; }
        }

        private string templateFileName;

        [XmlAttribute]
        public string DefaultConfigFilePath
        {
            get { return defaultConfigFilePath; }
            set { defaultConfigFilePath = value; }
        }

        private string defaultConfigFilePath;

        [XmlAttribute]
        public string DefaultOutputFilePath
        {
            get { return defaultOutputFilePath; }
            set { defaultOutputFilePath = value; }
        }

        private string defaultOutputFilePath;

        [XmlAttribute]
        public bool DefaultUseLocalAssembly
        {
            get { return defaultUseLocalAssembly; }
            set { defaultUseLocalAssembly = value; }
        }

        private bool defaultUseLocalAssembly;

        private bool defaultAutoGet;

        private bool defaultAutoPopulate;

        private int versionCount;


        [XmlAttribute]
        public int VersionCount
        {
            get { return versionCount; }
            set { versionCount = value; }
        }


        [XmlAttribute]
        public bool DefaultAutoGet
        {
            get { return defaultAutoGet; }
            set { defaultAutoGet = value; }
        }


        [XmlAttribute]
        public bool DefaultAutoPopulate
        {
            get { return defaultAutoPopulate; }
            set { defaultAutoPopulate = value; }
        }

        private bool autoSetSpecified;

        [XmlAttribute]
        public bool AutoSetSpecified
        {
            get { return autoSetSpecified; }
            set { autoSetSpecified = value; }
        }

        private bool simplifiedView;

        [XmlAttribute]
        public bool SimplifiedView
        {
            get { return simplifiedView; }
            set { simplifiedView = value; }
        }

        private bool hideSpecifiedFieldForOutput;

        [XmlAttribute]
        public bool HideSpecifiedFieldForOutput
        {
            get { return hideSpecifiedFieldForOutput; }
            set { hideSpecifiedFieldForOutput = value; }
        }
    }
}
