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