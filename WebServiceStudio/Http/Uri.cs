namespace IBS.Utilities.ASMWSTester.Http
{
    public class UriBase : IUri
    {
        protected string uri;

        public UriBase(string uri)
        {
            Populate(uri);
        }

        #region IUri Members

        public virtual string Uri
        {
            get { return uri; }
        }

        public virtual void Populate(string uri)
        {
            this.uri = uri;
        }

        #endregion
    }
}