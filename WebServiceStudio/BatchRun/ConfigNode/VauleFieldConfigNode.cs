namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class VauleFieldConfigNode : XmlConfigNodeBase
    {
        public VauleFieldConfigNode() : base()
        {
            nodeName = "vaulefield";
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}