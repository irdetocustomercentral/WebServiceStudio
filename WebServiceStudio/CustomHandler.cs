using System.ComponentModel;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class CustomHandler
    {
        public override string ToString()
        {
            return (handler + "{" + typeName + "}");
        }


        [XmlAttribute]
        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        [XmlAttribute]
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }


        private string handler;
        private string typeName;
    }
}