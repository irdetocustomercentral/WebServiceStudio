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
	




using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IBS.Utilities.ASMWSTester.BatchRun.ConfigNode;
using IBS.Utilities.ASMWSTester.XmlConfig;

namespace IBS.Utilities.ASMWSTester.BatchRun
{
    public static class BatchRunCongifFileHelper
    {
        public static TestConfigNode testConfigNode;

        public static void CreateTest(string name)
        {
            testConfigNode = new TestConfigNode();
            testConfigNode.Name = name;
        }

        public static void CreateRun(string method, string configFilePath)
        {
            RunConfigNode runConfigNode = new RunConfigNode();
            runConfigNode.Method = method;
            runConfigNode.ConfigFilePath = configFilePath;
            testConfigNode.Runs.Add(runConfigNode);
        }

        public static void RunSetValueField(string name, string xpath, string runMethod, string runConfigFilePath)
        {
            if (testConfigNode.GetCurrentRun() == null)
            {
                CreateRun(runMethod, runConfigFilePath);
            }

            SetValueFieldConfigNode setValueFieldConfigNode = new SetValueFieldConfigNode();
            setValueFieldConfigNode.Name = name;
            setValueFieldConfigNode.Xpath = xpath;

            if (testConfigNode != null)
            {
                testConfigNode.GetCurrentRun().SetvalueFields.Add(setValueFieldConfigNode);
            }
        }

        public static void RunParameter(string valuefieldname, string xpath, string runMethod, string runConfigFilePath)
        {
            if (testConfigNode.GetCurrentRun() == null)
            {
                CreateRun(runMethod, runConfigFilePath);
            }

            ParameterConfigNode parameter = new ParameterConfigNode();
            parameter.Valuefieldname = valuefieldname;
            parameter.Xpath = xpath;
            if (testConfigNode != null)
            {
                testConfigNode.GetCurrentRun().Parameters.Add(parameter);
            }
        }

        public static XmlDocument XmlDocumen
        {
            get { return xmlDocumen; }
            set { xmlDocumen = value; }
        }


        private static XmlDocument xmlDocumen = new XmlDocument();

        private static IDictionary<String, XmlDocument> xmlDocumens = new Dictionary<String, XmlDocument>();

        public static void CreateConfig(string filename, TreeNode selectNode)
        {
            if (!xmlDocumens.ContainsKey(filename))
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlCongifManager.ReadConfig(selectNode, null, ref xmlDocument);
                xmlDocumens.Add(filename, xmlDocument);
            }
        }

        //TODO Finish:save config file and batch config file
        public static void Save()
        {
            File.WriteAllText("BatchConfig.xml", testConfigNode.ToXML().OuterXml, Encoding.UTF8);

            IEnumerator ie = xmlDocumens.GetEnumerator();
            while (ie.MoveNext())
            {
                XmlDocument c = ie.Current as XmlDocument;
                //c.Save();
            }
        }
    }
}
