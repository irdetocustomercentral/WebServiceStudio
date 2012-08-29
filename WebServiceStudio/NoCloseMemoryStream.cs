using System.IO;

namespace IBS.Utilities.ASMWSTester
{
    internal class NoCloseMemoryStream : MemoryStream
    {
        public override void Close()
        {
        }
    }
}