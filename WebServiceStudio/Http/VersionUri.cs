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
