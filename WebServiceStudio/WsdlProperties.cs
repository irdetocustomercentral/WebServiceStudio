
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class WsdlProperties
    {
        public string[] GetProxyBaseTypeList()
        {
            return Configuration.MasterConfig.GetProxyBaseTypes();
        }

        public override string ToString()
        {
            return "";
        }


        [RefreshProperties(RefreshProperties.All), XmlAttribute]
        public string CustomCodeDomProvider
        {
            get { return ((language == Language.Custom) ? customCodeDomProvider : ""); }
            set
            {
                customCodeDomProvider = value;
                if ((value != null) && (value.Length > 0))
                {
                    language = Language.Custom;
                }
            }
        }

        [XmlAttribute]
        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        [XmlAttribute, RefreshProperties(RefreshProperties.All)]
        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        [XmlAttribute]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [XmlAttribute]
        public Protocol Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }

        [TypeConverter(typeof (ListStandardValues)), XmlAttribute]
        public string ProxyBaseType
        {
            get { return proxyBaseType; }
            set { proxyBaseType = value; }
        }

        [XmlAttribute]
        public string ProxyDomain
        {
            get { return proxyDomain; }
            set { proxyDomain = value; }
        }

        [XmlAttribute]
        public string ProxyPassword
        {
            get { return proxyPassword; }
            set { proxyPassword = value; }
        }

        [XmlAttribute]
        public string ProxyServer
        {
            get { return proxy; }
            set { proxy = value; }
        }

        [XmlAttribute]
        public string ProxyUserName
        {
            get { return proxyUserName; }
            set { proxyUserName = value; }
        }

        [XmlAttribute]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        private string customCodeDomProvider;
        private string domain;
        private Language language;
        private string password;
        private Protocol protocol;
        private string proxy;
        private string proxyBaseType;
        private string proxyDomain;
        private string proxyPassword;
        private string proxyUserName;
        private string userName;
    }
}
