	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class ParameterConfigNode : XmlConfigNodeBase
    {
        public ParameterConfigNode()
        {
            nodeName = "parameter";
        }

        private string xpath;
        private string valuefieldname;

        public string Xpath
        {
            get { return xpath; }
            set { xpath = value; }
        }

        public string Valuefieldname
        {
            get { return valuefieldname; }
            set { valuefieldname = value; }
        }
    }
}
