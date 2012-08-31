	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.IO;
using System.Net;
using System.Text;

namespace IBS.Utilities.ASMWSTester
{
    public class WSSWebResponse : WebResponse
    {
        public WSSWebResponse(WebResponse webResponse)
        {
            this.webResponse = webResponse;
            stream = new NoCloseMemoryStream();
            Stream stream1 = webResponse.GetResponseStream();
            byte[] buffer1 = new byte[0x400];
            while (true)
            {
                int num1 = stream1.Read(buffer1, 0, buffer1.Length);
                if (num1 <= 0)
                {
                    break;
                }
                stream.Write(buffer1, 0, num1);
            }
            stream.Position = 0;
        }

        public override void Close()
        {
            webResponse.Close();
        }

        public string DumpResponse()
        {
            long num1 = stream.Position;
            stream.Position = 0;
            string text1 = DumpResponse(this);
            stream.Position = num1;
            return text1;
        }

        public static string DumpResponse(WebResponse response)
        {
            Stream stream1 = response.GetResponseStream();
            StringBuilder builder1 = new StringBuilder();
            if (response is HttpWebResponse)
            {
                HttpWebResponse response1 = (HttpWebResponse) response;
                builder1.Append(
                    string.Concat(
                        new object[]
                            {"ResponseCode: ", (int) response1.StatusCode, " (", response1.StatusDescription, ")\n"}));
            }
            else if (response is WSSWebResponse)
            {
                WSSWebResponse response2 = (WSSWebResponse) response;
                builder1.Append(
                    string.Concat(
                        new object[]
                            {"ResponseCode: ", (int) response2.StatusCode, " (", response2.StatusDescription, ")\n"}));
            }
            foreach (string text1 in response.Headers.Keys)
            {
                builder1.Append(text1 + ":" + response.Headers[text1].ToString() + "\n");
            }
            builder1.Append("\n");
            builder1.Append(MessageTracer.ReadMessage(stream1, (int) response.ContentLength, response.ContentType));
            return builder1.ToString();
        }

        public override Stream GetResponseStream()
        {
            return stream;
        }


        public override long ContentLength
        {
            get { return webResponse.ContentLength; }
            set { webResponse.ContentLength = value; }
        }

        public override string ContentType
        {
            get { return webResponse.ContentType; }
            set { webResponse.ContentType = value; }
        }

        public override WebHeaderCollection Headers
        {
            get { return webResponse.Headers; }
        }

        public override Uri ResponseUri
        {
            get { return webResponse.ResponseUri; }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                if (webResponse is HttpWebResponse)
                {
                    return ((HttpWebResponse) webResponse).StatusCode;
                }
                return HttpStatusCode.NotImplemented;
            }
        }

        public string StatusDescription
        {
            get
            {
                if (webResponse is HttpWebResponse)
                {
                    return ((HttpWebResponse) webResponse).StatusDescription;
                }
                return "";
            }
        }


        private MemoryStream stream;
        private WebResponse webResponse;
    }
}
