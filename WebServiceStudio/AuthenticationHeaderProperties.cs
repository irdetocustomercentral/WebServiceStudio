
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.ComponentModel;
using System.Xml.Serialization;

namespace WebServiceStudio
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class AuthenticationHeaderProperties
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
        public string Dsn
        {
            get { return dsn; }
            set { dsn = value; }
        }

        [XmlAttribute]
        public string Proof
        {
            get { return proof; }
            set { proof = value; }
        }

        [XmlAttribute]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string dsn;
        private string proof;
        private string userName;
    }
}
