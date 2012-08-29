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