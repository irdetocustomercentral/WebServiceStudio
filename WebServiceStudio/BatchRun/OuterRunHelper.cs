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
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using IBS.Utilities.ASMWSTester.XmlConfig;

namespace IBS.Utilities.ASMWSTester.BatchRun
{
    /// <summary>
    /// 
    /// </summary>
    public static class OuterRunHelper
    {
        private static object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="configFilePath"></param>
        /// <param name="mainForm"></param>
        /// <returns></returns>
        public static XmlDocument InvokeMethod(string method, string configFilePath, MainForm mainForm)
        {
            #region Check Input

            if (configFilePath==null||configFilePath==String.Empty)
            {
                throw new Exception("Path of Config File can not be empty!");
            }

            #endregion

            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader streamReader = new StreamReader(configFilePath))
            {
                xmlDocument.Load(streamReader);
            }
            return InvokeMethod(method, xmlDocument, mainForm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="xmlDocument"></param>
        /// <param name="mainForm"></param>
        /// <returns></returns>
        public static XmlDocument InvokeMethod(string method, XmlDocument xmlDocument, MainForm mainForm)
        {
            #region Check Input

            if (method == null || method==String.Empty)
            {
                throw new Exception("Method not allow empty!");
            }

            if (xmlDocument==null)
            {
                throw new Exception("Config File not allow empty!");
            }

            if(mainForm==null)
            {
                throw new Exception("MainForm not allow null!");
            }

            #endregion


            #region write log

            Console.WriteLine(String.Format("WSDL EndPoint:{0}", mainForm.wsdl.Paths[0]));

            Console.WriteLine(String.Format("Method: {0} ", method ));

            Console.WriteLine(String.Format("ConfigFile: \r\n {0}", XmlCongifManager.FormatXmlDocumentToString(xmlDocument)));

            #endregion


            if (mainForm.treeMethods.Nodes.Count == 0)
            {
                throw new Exception("Can not get method tree ,please check asm path.");
            }

            lock (locker)
            {
                ClickNode(mainForm.treeMethods, mainForm.treeMethods.Nodes[0], method, mainForm);

                if(mainForm.treeInput.Nodes.Count==0)
                {
                    throw new Exception(String.Format("Not exist method named {0} in endpoint {1}", method, mainForm.textEndPointUri.Text));
                }

                XmlCongifManager.ApplyConfig(mainForm.treeInput.Nodes[0], String.Empty, xmlDocument,
                                             mainForm.wsdl.ProxyAssembly);

                mainForm.buttonInvoke_Click(null, null);

                XmlDocument xmlDocument1 = new XmlDocument();

                if (mainForm.treeOutput.Nodes[0].Nodes[1].Nodes.Count > 0)
                {
                    TreeNode outPutNode = mainForm.treeOutput.Nodes[0].Nodes[1].Nodes[0];
                    XmlCongifManager.ReadOutput(outPutNode, null, ref xmlDocument1);
                }

                return xmlDocument1;
            }
        }

        /// <summary>
        /// Create MainFrom
        /// </summary>
        /// <param name="asmPath">MainFrom's asmPath</param>
        /// <returns></returns>
        public static MainForm CreateMainForm(string asmPath)
        {
            MainForm mainForm = new MainForm();
            MainForm.mainForm = mainForm;
            WSSWebRequestCreate.RegisterPrefixes();
            mainForm.SetupAssemblyResolver();
            mainForm.OutCall = true;
            mainForm.chkLocalAssembly.Checked = true;
            lock (locker)
            {
                mainForm.tabMain.SelectTab("tabPageInvoke");
                mainForm.textEndPointUri.Text = asmPath;
                mainForm.buttonGet_Click(mainForm.buttonGet, null);
                return mainForm;
            }
        }

        /// <summary>
        /// Click the node
        /// </summary>
        /// <param name="treeview"></param>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="mainForm"></param>
        public static void ClickNode(TreeView treeview, TreeNode node, string name, MainForm mainForm)
        {
            treeview.SelectedNode = null;
            if (node.Tag != null)
            {
                string nodeType = node.Tag.GetType().ToString();

                if (nodeType == "System.Reflection.RuntimeMethodInfo")
                {
                    if (((MethodInfo) node.Tag).Name == name)
                    {
                        treeview.SelectedNode = node;
                        mainForm.treeMethods_AfterSelect(null, new TreeViewEventArgs(node));
                        return;
                    }
                }
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                ClickNode(treeview, childNode, name, mainForm);
            }
        }
    }
}
