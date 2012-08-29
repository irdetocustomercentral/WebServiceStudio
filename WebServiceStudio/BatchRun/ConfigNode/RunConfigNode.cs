using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
{
    public class RunConfigNode : XmlConfigNodeBase
    {
        public RunConfigNode()
        {
            nodeName = "run";
        }


        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        public string ConfigFilePath
        {
            get { return configFilePath; }
            set { configFilePath = value; }
        }


        public IList<SetValueFieldConfigNode> SetvalueFields
        {
            get { return setvalueFields; }
            set { setvalueFields = value; }
        }

        public IList<ParameterConfigNode> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private string method;
        private string configFilePath;
        private IList<SetValueFieldConfigNode> setvalueFields = new List<SetValueFieldConfigNode>();
        private IList<ParameterConfigNode> parameters = new List<ParameterConfigNode>();
    }
}