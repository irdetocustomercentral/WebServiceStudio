	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class ModuleConfigNode : XmlConfigNodeBase
    {
        public ModuleConfigNode() : base()
        {
            nodeName = "module";
        }


        public IList<TestConfigNode> Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        private IList<TestConfigNode> tests = new List<TestConfigNode>();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string name;
    }
}
