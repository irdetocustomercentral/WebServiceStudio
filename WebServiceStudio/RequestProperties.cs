	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System.Net;
using System.Web.Services.Protocols;

namespace IBS.Utilities.ASMWSTester
{
    internal class RequestProperties
    {
        public RequestProperties(HttpWebClientProtocol proxy)
        {
            allowAutoRedirect = true;
            allowWriteStreamBuffering = true;
            timeout = 0x2710;
            if (proxy != null)
            {
                Method = HttpMethod.POST;
                preAuthenticate = proxy.PreAuthenticate;
                timeout = proxy.Timeout;
                useCookieContainer = proxy.CookieContainer != null;
                SoapHttpClientProtocol protocol1 = proxy as SoapHttpClientProtocol;
                if (protocol1 != null)
                {
                    allowAutoRedirect = protocol1.AllowAutoRedirect;
                    allowWriteStreamBuffering = protocol1.AllowAutoRedirect;
                    WebProxy proxy1 = protocol1.Proxy as WebProxy;
                    HttpProxy = ((proxy1 != null) && (proxy1.Address != null)) ? proxy1.Address.ToString() : null;
                }
            }
        }


        public string __RequestProperties__
        {
            get { return ""; }
        }

        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        public bool AllowWriteStreamBuffering
        {
            get { return allowWriteStreamBuffering; }
            set { allowWriteStreamBuffering = value; }
        }

        public string BasicAuthPassword
        {
            get { return basicAuthPassword; }
            set { basicAuthPassword = value; }
        }

        public string BasicAuthUserName
        {
            get { return basicAuthUserName; }
            set { basicAuthUserName = value; }
        }

        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        public string HttpProxy
        {
            get { return proxy; }
            set { proxy = ((value == null) || (value.Length == 0)) ? null : new WebProxy(value).Address.ToString(); }
        }

        public bool KeepAlive
        {
            get { return keepAlive; }
            set { keepAlive = value; }
        }

        public HttpMethod Method
        {
            get { return method; }
            set { method = value; }
        }

        public bool Pipelined
        {
            get { return pipelined; }
            set { pipelined = value; }
        }

        public bool PreAuthenticate
        {
            get { return preAuthenticate; }
            set { preAuthenticate = value; }
        }

        public bool SendChunked
        {
            get { return sendChunked; }
            set { sendChunked = value; }
        }

        public string SOAPAction
        {
            get { return soapAction; }
            set { soapAction = value; }
        }

        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public bool UseCookieContainer
        {
            get { return useCookieContainer; }
            set { useCookieContainer = value; }
        }

        public bool UseDefaultCredential
        {
            get { return useDefaultCredential; }
            set { useDefaultCredential = value; }
        }


        public bool allowAutoRedirect;
        public bool allowWriteStreamBuffering;
        public string basicAuthPassword;
        public string basicAuthUserName;
        public string contentType;
        public bool keepAlive;
        public HttpMethod method;
        public bool pipelined;
        public bool preAuthenticate;
        public string proxy;
        public string requestPayLoad;
        public string responsePayLoad;
        public bool sendChunked;
        public string soapAction;
        public int timeout;
        public string url;
        public bool useCookieContainer;
        public bool useDefaultCredential;


        public enum HttpMethod
        {
            GET,
            POST
        }
    }
}
