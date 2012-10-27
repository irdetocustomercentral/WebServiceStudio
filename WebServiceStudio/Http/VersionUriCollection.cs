
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System.Collections.Generic;

namespace WebServiceStudio.Http
{
    public class VersionUriCollection : List<VersionUri>
    {
        public VersionUri this[string version]
        {
            get
            {
                return Find(
                    delegate(VersionUri item) { return ((item != null) && Equals(item.VersionName, version)); }
                    );
            }
        }

        public List<string> Versions
        {
            get
            {
                List<string> versions = new List<string>();
                foreach (VersionUri versionUri in this)
                {
                    versions.Add(versionUri.VersionName);
                }

                return versions;
            }
        }
    }
}
