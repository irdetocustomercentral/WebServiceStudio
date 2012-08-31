<<<<<<< HEAD
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
	




=======
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
>>>>>>> SandBox
using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.BatchRun.ConfigNode
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
