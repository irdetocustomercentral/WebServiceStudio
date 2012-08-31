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
