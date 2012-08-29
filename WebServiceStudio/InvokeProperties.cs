using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class InvokeProperties
    {

        private ArrayList uris;

        public InvokeProperties()
        {
            uris = new ArrayList();
        }

        public void AddUri(string uri)
        {
            uris.Remove(uri);
            uris.Insert(0, uri);
            Configuration.SaveMasterConfig();
        }

        public override string ToString()
        {
            return "";
        }


        [XmlArrayItem("Uri", typeof (string)), Browsable(false)]
        public string[] RecentlyUsedUris
        {
            get { return (uris.ToArray(typeof (string)) as string[]); }
            set
            {
                uris.Clear();
                if (value != null)
                {
                    uris.AddRange(value);
                }
            }
        }
    }
}