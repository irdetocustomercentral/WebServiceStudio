
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.Collections.Generic;
//using ASMWSTester.BatchRun;
using IBS.Utilities.ASMWSTester.BatchRun;
using IBS.Utilities.ASMWSTester;

namespace IBS.Utilities.ASMWSTester.BatchRun
{
    public static class MainFormFactory
    {
        private static object locker = new object();

        public static MainForm CreateMainFrom(string asmPath)
        {
            lock (locker)
            {
                if (asmPath != null)
                {
                    if (asmPath != String.Empty)
                    {
                        if (!mainFromList.ContainsKey(asmPath))
                        {
                            MainForm newMainFrom = OuterRunHelper.CreateMainForm(asmPath);
                            mainFromList.Add(asmPath, newMainFrom);
                        }
                    }
                }

                return mainFromList[asmPath];
            }
        }

        private static IDictionary<String, MainForm> mainFromList = new Dictionary<String, MainForm>();
    }
}
