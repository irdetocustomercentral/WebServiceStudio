
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.Collections.Generic;

namespace WebServiceStudio.Http
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
