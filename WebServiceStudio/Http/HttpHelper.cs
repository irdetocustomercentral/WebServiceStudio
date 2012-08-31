<<<<<<< HEAD
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
	




=======
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
>>>>>>> SandBox
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace IBS.Utilities.ASMWSTester.Http
{
    public static class HttpHelper
    {
        private const string UriDelimiter = "/";

        public static string GetRegexMatchString(string input, string pattern)
        {
            return Regex.Match(input, pattern).Value;
        }

        public static List<string> GetSubUris(string parentUri, string rootUri)
        {
            Regex regObj = new Regex("(?<=<a href=\")[^\"\\r\\n]*(?=\">)",
                                     RegexOptions.RightToLeft);

            MatchCollection matches = regObj.Matches(GetUriContent(parentUri).ToLower());

            List<string> subUris = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Value != "/")
                {
                    subUris.Add(CombineUris(parentUri, Format(match.Value)));
                }
            }

            return subUris;
        }

        public static string GetUriContent(string uri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);

            try
            {
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                Encoding enc = Encoding.GetEncoding(1252);
                StreamReader responseStream =
                    new StreamReader(response.GetResponseStream(), enc);

                string ret = responseStream.ReadToEnd();

                response.Close();
                responseStream.Close();

                return ret;
            }
            catch(Exception ex)
            {
                return String.Empty;
            }
        }

        public static string GetRootUriPath(string uri)
        {
            Regex regObj = new Regex("http://[^/\r\n]*/",
                                     RegexOptions.RightToLeft);

            Match match = regObj.Match(uri);

            if (match != null)
            {
                return match.Value;
            }
            else
            {
                return null;
            }
        }

        public static List<string> GetSvcUris(string uri)
        {
            List<string> svcUris = new List<string>();

            GetSvcUrisRecursive(uri, svcUris);

            return svcUris;
        }

        private static void GetSvcUrisRecursive(string uri, List<string> svcUris)
        {
            List<string> subUris = GetSubUris(uri, GetRootUriPath(uri));

            if (subUris != null && subUris.Count > 0)
            {
                foreach (string subUri in subUris)
                {
                    if (subUri != null)
                    {
                        if (subUri.Trim().ToLower().EndsWith("bin/") ||
                            subUri.Trim().ToLower().EndsWith("logs/"))
                        {
                            continue;
                        }
                        else if (subUri.Trim().ToLower().EndsWith(UriDelimiter))
                        {
                            string dirUri = subUri.Trim().ToLower();
                            string parentUri = uri.Trim().ToLower();

                            if (dirUri.Contains(parentUri))
                            {
                                GetSvcUrisRecursive(subUri, svcUris);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (subUri.Trim().ToLower().EndsWith(".svc"))
                        {
                            svcUris.Add(subUri);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private static string CombineUris(string parentUri, string path)
        {
            string tmpRoot = parentUri.Trim().ToLower();
            string tmpPath = path.Trim().ToLower();

            if (tmpPath.StartsWith(UriDelimiter))
            {
                tmpRoot = GetRootUriPath(tmpRoot);
                tmpPath = tmpPath.Substring(1);
            }

            if (tmpRoot.EndsWith(UriDelimiter))
            {
                tmpRoot = tmpRoot.Substring(0, tmpRoot.Length - 1);
            }

            return tmpRoot + UriDelimiter + tmpPath;
        }

        private static string Format(string uri)
        {
            return uri.Replace(@"\", "/");
        }
    }
}
