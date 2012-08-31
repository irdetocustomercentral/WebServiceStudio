	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
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
