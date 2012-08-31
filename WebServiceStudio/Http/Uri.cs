
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

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
