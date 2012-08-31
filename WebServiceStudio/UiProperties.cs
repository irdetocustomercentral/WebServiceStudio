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
using System.Drawing;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class UiProperties
    {
        static UiProperties()
        {
            fontConverter = new FontConverter();
        }

        public override string ToString()
        {
            return "";
        }


        [XmlIgnore, TypeConverter(typeof (FontConverter))]
        public Font MessageFont
        {
            get
            {
                if (messageFont == null)
                {
                    messageFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                return messageFont;
            }
            set { messageFont = value; }
        }

        [Browsable(false), XmlElement("MessageFont")]
        public string MessageFontX
        {
            get { return (string) fontConverter.ConvertTo(null, null, messageFont, typeof (string)); }
            set { messageFont = (Font) fontConverter.ConvertFrom(null, null, value); }
        }

        [TypeConverter(typeof (FontConverter)), XmlIgnore]
        public Font ReqRespFont
        {
            get
            {
                if (reqRespFont == null)
                {
                    reqRespFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                return reqRespFont;
            }
            set { reqRespFont = value; }
        }

        [Browsable(false), XmlElement("ReqRespFont")]
        public string ReqRespFontX
        {
            get { return (string) fontConverter.ConvertTo(null, null, reqRespFont, typeof (string)); }
            set { reqRespFont = (Font) fontConverter.ConvertFrom(null, null, value); }
        }

        [XmlIgnore, TypeConverter(typeof (FontConverter))]
        public Font WsdlFont
        {
            get
            {
                if (wsdlFont == null)
                {
                    wsdlFont = new Font("Lucida Sans Unicode", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                return wsdlFont;
            }
            set { wsdlFont = value; }
        }

        [XmlElement("WsdlFont"), Browsable(false)]
        public string WsdlFontX
        {
            get { return (string) fontConverter.ConvertTo(null, null, wsdlFont, typeof (string)); }
            set { wsdlFont = (Font) fontConverter.ConvertFrom(null, null, value); }
        }


        private static FontConverter fontConverter;
        private Font messageFont;
        private Font reqRespFont;
        private Font wsdlFont;
    }
}
