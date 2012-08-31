	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.Http
{
    public class UriResolver : IUriResolver
    {
        #region IUriResolver Members

        public VersionUriCollection GetVersionUriCollection(string buildUri)
        {
            string rootUri = HttpHelper.GetRootUriPath(buildUri);

            List<string> subUris = HttpHelper.GetSubUris(buildUri, rootUri);


            VersionUriCollection versionUris = new VersionUriCollection();

            int versionCount = Configuration.MasterConfig.OtherSettings.VersionCount;

            int count = 0;

            if (versionCount > subUris.Count)
            {
                count = subUris.Count;
            }
            else
            {
                count = versionCount;
            }


            for (int i = 0; i < count; i++)
            {
                versionUris.Add(new VersionUri(subUris[i]));
            }

            return versionUris;
        }

        public VersionUri GetLatestVersionUri(string latestUri, string versionName)
        {
            VersionUri latest = new VersionUri(latestUri);
            latest.VersionName = versionName;

            return latest;
        }

        #endregion
    }
}
