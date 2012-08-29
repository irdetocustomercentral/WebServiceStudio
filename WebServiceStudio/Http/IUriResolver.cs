namespace IBS.Utilities.ASMWSTester.Http
{
    internal interface IUriResolver
    {
        VersionUriCollection GetVersionUriCollection(string buildUri);

        VersionUri GetLatestVersionUri(string latestUri, string versionName);
    }
}