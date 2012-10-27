
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.ComponentModel;
using System.Xml.Serialization;

namespace WebServiceStudio
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
