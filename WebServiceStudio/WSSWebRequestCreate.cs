using System;
using System.Net;

namespace IBS.Utilities.ASMWSTester
{
    public class WSSWebRequestCreate : IWebRequestCreate
    {
        public virtual WebRequest Create(Uri uri)
        {
            WebRequest request1 = WebRequest.CreateDefault(uri);
            if (WSSWebRequest.RequestTrace == null)
            {
                return request1;
            }
            return new WSSWebRequest(request1);
        }

        public static void RegisterPrefixes()
        {
            WSSWebRequestCreate create1 = new WSSWebRequestCreate();
            WebRequest.RegisterPrefix("http://", create1);
            WebRequest.RegisterPrefix("https://", create1);
        }
    }
}