
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class SetValueFieldConfigNode : XmlConfigNodeBase
    {
        public SetValueFieldConfigNode()
        {
            nodeName = "setvaluefield";
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Xpath
        {
            get { return xpath; }
            set { xpath = value; }
        }

        private string name;
        private string xpath;
    }
}
