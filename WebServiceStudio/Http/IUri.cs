namespace IBS.Utilities.ASMWSTester.Http
{
    internal interface IUri
    {
        string Uri { get; }

        void Populate(string uri);
    }
}