
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WebServiceStudio
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ServerHistoryProperties
    {
        private List<string> histList;

        public ServerHistoryProperties()
        {
            histList = new List<string>();
        }

        public void AddHistory(string servStr)
        {
            histList.Remove(servStr);
            histList.Insert(0, servStr);
            Configuration.SaveMasterConfig();
        }

        public override string ToString()
        {
            return "";
        }

        [XmlArrayItem("server", typeof(string))]
        public string[] CalledServer
        {
            get { 
                return histList.ToArray();
            }

            set
            {
                histList.Clear();
                if (value != null)
                {
                    histList.AddRange(value);
                }
            }
        }

    }
}

