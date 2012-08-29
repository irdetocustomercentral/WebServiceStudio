using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
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
