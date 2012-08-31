	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.Collections.Generic;
using System.Xml;


namespace IBS.Utilities.ASMWSTester.BatchRun
{
    public static class BatchRunHelper
    {
        private static XmlNodeList asmPathNodeList;
        private static object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static void RunTest(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            IDictionary<String, String> valueFields = new Dictionary<String, String>();

            XmlNodeList runNodeList = xmlDocument.SelectNodes("test/runs/run");

            foreach (XmlNode runNode in runNodeList)
            {
                MainForm mainForm = MainFormFactory.CreateMainFrom(GetAsmPath(runNode.Attributes["asmPath"].Value));
                Invoke(runNode, mainForm, valueFields);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="mainForm"></param>
        /// <param name="valueFields"></param>
        /// <returns></returns>
        private static XmlDocument Invoke(XmlNode xmlNode, MainForm mainForm, IDictionary<String, String> valueFields)
        {
            XmlDocument result;

            XmlDocument configFileDocument = new XmlDocument();
            configFileDocument.Load(xmlNode.Attributes["configfilepath"].Value);

            XmlNodeList parameterNodeList = xmlNode.SelectNodes("parameters/parameter");

            XmlNodeList setvaluefieldNodeList = xmlNode.SelectNodes("setvaluefields/setvaluefield");

            XmlNodeList assertNodeList = xmlNode.SelectNodes("asserts/assert");

            XmlNodeList logList = xmlNode.SelectNodes("logs/log");

            HandleParmeter(configFileDocument, parameterNodeList, valueFields);

            result = OuterRunHelper.InvokeMethod(xmlNode.Attributes["method"].Value,
                                                 configFileDocument,
                                                 mainForm);

            HandleAssert(assertNodeList, result);

            HandleSetvaluefield(result, setvaluefieldNodeList, valueFields);

            HandleLog(result, logList, valueFields);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="logList"></param>
        /// <param name="valueFields"></param>
        private static void HandleLog(XmlDocument result, XmlNodeList logList, IDictionary<String, String> valueFields)
        {
            foreach (XmlNode logNode in logList)
            {
                if (logNode.Attributes["message"] != null)
                {
                    string logmessage = logNode.Attributes["message"].Value;

                    if (logmessage.IndexOf('[') > -1 && logmessage.IndexOf(']') > -1) //valuefield
                    {
                        int start = logmessage.IndexOf('[');
                        int end = logmessage.IndexOf(']');

                        string oldStr = logmessage.Substring(start, end - start + 1);
                        string key = oldStr.Substring(1, oldStr.Length - 2);

                        Console.WriteLine(logmessage.Replace(oldStr, valueFields[key]));
                    }
                    else if (logmessage.IndexOf('(') > -1 && logmessage.IndexOf(')') > -1) //xpath
                    {
                        int start = logmessage.IndexOf('(');
                        int end = logmessage.IndexOf(')');

                        string oldStr = logmessage.Substring(start, end - start + 1);
                        string key = oldStr.Substring(1, oldStr.Length - 2);


                        XmlNode node = result.SelectSingleNode(key);
                        if (node != null)
                        {
                            Console.WriteLine(logmessage.Replace(oldStr, node.Attributes["value"].Value));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="setvaluefieldNodeList"></param>
        /// <param name="valueFields"></param>
        private static void HandleSetvaluefield(XmlDocument result, XmlNodeList setvaluefieldNodeList,
                                                IDictionary<string, string> valueFields)
        {
            foreach (XmlNode returnNode in setvaluefieldNodeList)
            {
                if (returnNode.Attributes["log"] != null)
                {
                    Console.WriteLine(returnNode.Attributes["log"].Value);
                }
                XmlNode node = result.SelectSingleNode(returnNode.Attributes["xpath"].Value);
                if (node != null)
                {
                    valueFields.Add(returnNode.Attributes["name"].Value,
                                    node.Attributes["value"].Value);
                    Console.WriteLine(
                        String.Format("SetValuefield:valuefield {0},value {1}", returnNode.Attributes["name"].Value,
                                      node.Attributes["value"].Value));
                }
                else
                {
                    throw new Exception(String.Format("xpath {0} error!", returnNode.Attributes["xpath"].Value));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assertNodeList"></param>
        /// <param name="result"></param>
        private static void HandleAssert(XmlNodeList assertNodeList, XmlDocument result)
        {
            foreach (XmlNode assertNode in assertNodeList)
            {
                if (assertNode.Attributes["log"] != null)
                {
                    Console.WriteLine(assertNode.Attributes["log"].Value);
                }

                string[] xPaths = new String[1];
                xPaths[0] = assertNode.Attributes["actual"].Value;

                string assertType = assertNode.Attributes["type"].Value;
                string expected = assertNode.Attributes["expected"].Value;
                string message = assertNode.Attributes["message"].Value;
                string actualattribute = String.Empty;

                if (assertNode.Attributes["actualattribute"] != null)
                {
                    actualattribute = assertNode.Attributes["actualattribute"].Value;
                }

                string[] xPath1s = null;

                ParseXPath(xPaths, result, ref xPath1s);

                if (xPath1s != null)
                {
                    for (int i = 0; i < xPath1s.Length; i++)
                    {
                        RunAssert(result, xPath1s[i], assertType, expected, message, actualattribute);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPaths"></param>
        /// <param name="result"></param>
        /// <param name="xPath1s"></param>
        public static void ParseXPath(string[] xPaths, XmlDocument result, ref string[] xPath1s)
        {
            int position = xPaths[0].IndexOf("*");

            if (position > -1)
            {
                string left = xPaths[0].Substring(0, position);
                string right = xPaths[0].Substring(position + 1);

                int length =
                    Convert.ToInt32(
                        result.SelectSingleNode(left.Substring(0, left.LastIndexOf("/"))).Attributes["length"].Value);

                xPath1s = new string[length];

                for (int i = 0; i < length; i++)
                {
                    xPath1s[i] = left + i + right;
                }

                string[] newXPath = null;

                ParseXPath(xPath1s, result, ref newXPath);
            }
            else
            {
                xPath1s = xPaths;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="actual"></param>
        /// <param name="assertType"></param>
        /// <param name="expected"></param>
        /// <param name="message"></param>
        /// <param name="actualattribute"></param>
        public static void RunAssert(XmlDocument result, string actual, string assertType, string expected,
                                     string message,
                                     string actualattribute)
        {
            XmlNode node = result.SelectSingleNode(actual);
            if (node != null)
            {
                string actualvalue = String.Empty;

                if (actualattribute != String.Empty)
                {
                    if (node.Attributes[actualattribute] != null)
                    {
                        actualvalue = node.Attributes[actualattribute].Value;
                    }
                    else
                    {
                        throw new Exception(String.Format("Node {0} do not have attribute {1}", actual, actualattribute));
                    }
                }
                else
                {
                    actualattribute = "value";
                    actualvalue = node.Attributes[actualattribute].Value;
                }

                Console.WriteLine(
                    String.Format("Assert:type {0},expected {1},actual {2},xpath {3},attribute {4}", assertType,
                                  expected, actualvalue, actual, actualattribute));

              
            }
            else
            {
                throw new Exception(String.Format("xpath {0} error!", actual));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFileDocument"></param>
        /// <param name="parameterNodeList"></param>
        /// <param name="valueFields"></param>
        private static void HandleParmeter(XmlDocument configFileDocument, XmlNodeList parameterNodeList,
                                           IDictionary<string, string> valueFields)
        {
            foreach (XmlNode parameterNode in parameterNodeList)
            {
                if (parameterNode.Attributes["log"] != null)
                {
                    Console.WriteLine(parameterNode.Attributes["log"].Value);
                }
                XmlAttribute valueFieldAttribute = parameterNode.Attributes["valuefieldname"];
                if (valueFieldAttribute != null)
                {
                    XmlNode node = configFileDocument.SelectSingleNode(parameterNode.Attributes["xpath"].Value);

                    if (node != null)
                    {
                        if (node.Attributes["value"] != null)
                        {
                            if (valueFields.ContainsKey(valueFieldAttribute.Value))
                            {
                                node.Attributes["value"].Value
                                    = valueFields[valueFieldAttribute.Value];
                                Console.WriteLine(
                                    String.Format("SetParmeter:parmeter {1},valueFields[{0}],value {2}",
                                                  valueFieldAttribute.Value, parameterNode.Attributes["xpath"].Value,
                                                  valueFields[valueFieldAttribute.Value]));
                            }
                            else
                            {
                                throw new Exception(String.Format("Not exist ValueField {0}", valueFieldAttribute.Value));
                            }
                        }
                        else
                        {
                            throw new Exception(
                                String.Format("{0} not exist value Attribute", parameterNode.Attributes["xpath"].Value));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("xpath {0} error!", parameterNode.Attributes["xpath"].Value));
                    }
                }
                else
                {
                    XmlNode node = configFileDocument.SelectSingleNode(parameterNode.Attributes["xpath"].Value);

                    if (node != null)
                    {
                        if (node.Attributes["value"] != null)
                        {
                            node.Attributes["value"].Value
                                = parameterNode.Attributes["value"].Value;
                            Console.WriteLine(
                                String.Format("SetParmeter:parmeter {0},value {1}",
                                              parameterNode.Attributes["xpath"].Value,
                                              parameterNode.Attributes["value"].Value));
                        }
                        else
                        {
                            throw new Exception(
                                String.Format("{0} not exist value Attribute", parameterNode.Attributes["xpath"].Value));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("xpath {0} error!", parameterNode.Attributes["xpath"].Value));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetAsmPath(string key)
        {
            lock (locker)
            {
                if (asmPathNodeList == null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(@"AsmPaths.xml");
                    asmPathNodeList = xmlDocument.SelectNodes(@"asmPaths/asmPath");
                }
            }

            foreach (XmlNode amsPathNode in asmPathNodeList)
            {
                if (amsPathNode.Attributes["name"].Value == key)
                {
                    return amsPathNode.Attributes["value"].Value;
                }
            }

            throw new Exception("Asm Path not exist.");
        }
    }
}
