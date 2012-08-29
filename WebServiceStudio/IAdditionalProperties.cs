using System.Web.Services.Protocols;

namespace IBS.Utilities.ASMWSTester
{
    public interface IAdditionalProperties
    {
        void UpdateProxy(HttpWebClientProtocol proxy);
    }
}