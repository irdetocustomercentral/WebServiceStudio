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
	




namespace IBS.Utilities.ASMWSTester.Http
{
    public class ModuleUri : UriBase
    {
        //private const string MODULE_PATTERN = @"\w+/\w+\.svc\b";

        private string moduleName;

        public ModuleUri(string uri) : base(uri)
        {
        }

        public virtual string RootUri
        {
            get { return HttpHelper.GetRootUriPath(Uri); }
        }

        public string ModuleName
        {
            get { return moduleName; }
        }

        public override void Populate(string uri)
        {
            base.Populate(uri);

            string[] strs=uri.Split('/');

            moduleName = strs[strs.Length - 2] + "/" + strs[strs.Length-1];

            //moduleName = HttpHelper.GetRegexMatchString(uri, MODULE_PATTERN);
        }
    }
}
