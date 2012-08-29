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