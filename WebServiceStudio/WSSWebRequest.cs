	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.IO;
using System.Net;

namespace IBS.Utilities.ASMWSTester
{
    public class WSSWebRequest : WebRequest
    {
        public WSSWebRequest(WebRequest webRequest)
        {
            this.webRequest = webRequest;
            stream = new NoCloseMemoryStream();
        }

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object asyncState)
        {
            throw new NotSupportedException();
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object asyncState)
        {
            throw new NotSupportedException();
        }

        public override Stream EndGetRequestStream(IAsyncResult result)
        {
            throw new NotSupportedException();
        }

        public override WebResponse EndGetResponse(IAsyncResult result)
        {
            throw new NotSupportedException();
        }

        public override Stream GetRequestStream()
        {
            return stream;
        }

        public override WebResponse GetResponse()
        {
            WebResponse response4;
            requestProperties.contentType = webRequest.ContentType;
            requestProperties.soapAction = webRequest.Headers["SOAPAction"];
            requestProperties.url = webRequest.RequestUri.ToString();
            if (webRequest.Method.ToUpper() == "POST")
            {
                requestProperties.Method = RequestProperties.HttpMethod.POST;
                Stream stream1 = webRequest.GetRequestStream();
                stream1.Write(stream.GetBuffer(), 0, (int) stream.Length);
                stream1.Close();
                stream.Position = 0;
                requestProperties.requestPayLoad = MessageTracer.ReadMessage(stream, requestProperties.contentType);
            }
            else if (webRequest.Method.ToUpper() == "GET")
            {
                requestProperties.Method = RequestProperties.HttpMethod.GET;
            }
            try
            {
                WebResponse response1 = webRequest.GetResponse();
                WSSWebResponse response2 = new WSSWebResponse(response1);
                requestProperties.responsePayLoad = response2.DumpResponse();
                response4 = response2;
            }
            catch (WebException exception1)
            {
                if (exception1.Response != null)
                {
                    WSSWebResponse response3 = new WSSWebResponse(exception1.Response);
                    requestProperties.responsePayLoad = response3.DumpResponse();
                    throw new WebException(exception1.Message, exception1, exception1.Status, response3);
                }
                requestProperties.responsePayLoad = exception1.ToString();
                throw;
            }
            catch (Exception exception2)
            {
                requestProperties.responsePayLoad = exception2.ToString();
                throw;
            }
            return response4;
        }


        public override string ConnectionGroupName
        {
            get { return webRequest.ConnectionGroupName; }
            set { webRequest.ConnectionGroupName = value; }
        }

        public override long ContentLength
        {
            get { return webRequest.ContentLength; }
            set { webRequest.ContentLength = value; }
        }

        public override string ContentType
        {
            get { return webRequest.ContentType; }
            set { webRequest.ContentType = value; }
        }

        public override ICredentials Credentials
        {
            get { return webRequest.Credentials; }
            set { webRequest.Credentials = value; }
        }

        public override WebHeaderCollection Headers
        {
            get { return webRequest.Headers; }
            set { webRequest.Headers = value; }
        }

        public override string Method
        {
            get { return webRequest.Method; }
            set { webRequest.Method = value; }
        }

        public override bool PreAuthenticate
        {
            get { return webRequest.PreAuthenticate; }
            set { webRequest.PreAuthenticate = value; }
        }

        public override IWebProxy Proxy
        {
            get { return webRequest.Proxy; }
            set { webRequest.Proxy = value; }
        }

        internal static RequestProperties RequestTrace
        {
            get { return requestProperties; }
            set { requestProperties = value; }
        }

        public override Uri RequestUri
        {
            get { return webRequest.RequestUri; }
        }

        public override int Timeout
        {
            get { return webRequest.Timeout; }
            set { webRequest.Timeout = value; }
        }


        private static RequestProperties requestProperties;
        private MemoryStream stream;
        private WebRequest webRequest;
    }
}
