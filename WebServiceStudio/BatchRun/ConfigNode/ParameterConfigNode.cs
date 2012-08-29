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