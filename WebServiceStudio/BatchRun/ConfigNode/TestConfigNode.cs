
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.Collections.Generic;

namespace WebServiceStudio.BatchRun.ConfigNode
{
    public class TestConfigNode : XmlConfigNodeBase
    {
        public TestConfigNode()
        {
            nodeName = "test";
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string name;

        private IList<RunConfigNode> runs = new List<RunConfigNode>();

        public IList<RunConfigNode> Runs
        {
            get { return runs; }
            set { runs = value; }
        }

        public IList<VauleFieldConfigNode> ValueFields
        {
            get { return valueFields; }
            //set { valueFields = value; }
        }

        private IList<VauleFieldConfigNode> valueFields = new List<VauleFieldConfigNode>();

        public RunConfigNode GetCurrentRun()
        {
            if (runs.Count > 0)
            {
                return runs[runs.Count - 1];
            }
            else
            {
                return null;
            }
        }
    }
}
