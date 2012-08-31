	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System.Collections.Generic;

namespace IBS.Utilities.ASMWSTester.Http
{
    public class VersionUri : UriBase
    {
        //private const string VERSION_PATTERN = @"\d+\.\d+\.\d+\.\d*";

        private string versionName;
        private ModuleUriCollection serviceUris = new ModuleUriCollection();

        public VersionUri(string uri) : base(uri)
        {
        }

        public string VersionName
        {
            get { return versionName; }

            set { versionName = value; }
        }

        public ModuleUriCollection ServiceUris
        {
            get { return serviceUris; }
        }

        public override void Populate(string uri)
        {
            base.Populate(uri);

            //versionName = HttpHelper.GetRegexMatchString(uri, VERSION_PATTERN);

            if (uri.Length == uri.LastIndexOf("/")+1)
            {
                uri = uri.Substring(0, uri.Length - 1);
            }

            versionName = uri.Substring(uri.LastIndexOf("/")+1);           

            List<string> svcUris = HttpHelper.GetSvcUris(uri);

            foreach (string svcUri in svcUris)
            {
                serviceUris.Add(new ModuleUri(svcUri));
            }
        }
    }
}
