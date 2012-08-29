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