using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.Http
{
    public class ModuleUriCollection : List<ModuleUri>
    {
        public ModuleUri this[string module]
        {
            get
            {
                return Find(
                    delegate(ModuleUri item) { return ((item != null) && Equals(item.ModuleName, module)); }
                    );
            }
        }

        public List<string> Modules
        {
            get
            {
                List<string> modules = new List<string>();
                foreach (ModuleUri serviceUri in this)
                {
                    modules.Add(serviceUri.ModuleName);
                }
                modules.Sort();
                return modules;
            }
        }
    }
}