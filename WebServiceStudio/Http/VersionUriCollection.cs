using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.Http
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