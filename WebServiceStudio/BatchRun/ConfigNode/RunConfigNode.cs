	//WebServiceStudio Application to test WCF web services.

	//Copyright (C) 2012  Irdeto Customer Care And Billing

	//This program is free software: you can redistribute it and/or modify
	//it under the terms of the GNU General Public License as published by
	//the Free Software Foundation, either version 3 of the License, or
	//(at your option) any later version.

	//This program is distributed in the hope that it will be useful,
	//but WITHOUT ANY WARRANTY; without even the implied warranty of
	//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//GNU General Public License for more details.

	//You should have received a copy of the GNU General Public License
	//along with this program.  If not, see http://www.gnu.org/licenses/

	//Web service Studio has been modifided to understand more complex input types
	//such as Iredeto's Customer Care input type of Base Query Request. 
	




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
