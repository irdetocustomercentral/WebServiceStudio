
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.Collections;
using System.Net;
using System.Web.Services.Protocols;

namespace WebServiceStudio
{
    internal class ProxyProperties
    {
        static ProxyProperties()
        {
            typeNotFoundMessage =
                "ProxyPropertiesType {0} specified in WebServiceStudio.exe.options is not found";
        }

        public ProxyProperties()
        {
        }

        public ProxyProperties(HttpWebClientProtocol proxy)
        {
            Timeout = proxy.Timeout;
            AllowAutoRedirect = proxy.AllowAutoRedirect;
            PreAuthenticate = proxy.PreAuthenticate;
            if (proxy.CookieContainer == null)
            {
                UseCookieContainer = true;
            }
            Server = new ServerProperties();
            Server.Url = proxy.Url;
            SetCredentialValues(proxy.Credentials, new Uri(Server.Url), out Server.UseDefaultCredentials,
                                out Server.UserNameForBasicAuth, out Server.PasswordForBasicAuth);
            WebProxy proxy1 = proxy.Proxy as WebProxy;
            if (proxy1 != null)
            {
                HttpProxy = new ServerProperties();
                HttpProxy.Url = proxy1.Address.ToString();
                SetCredentialValues(proxy1.Credentials, new Uri(HttpProxy.Url), out HttpProxy.UseDefaultCredentials,
                                    out HttpProxy.UserNameForBasicAuth, out HttpProxy.PasswordForBasicAuth);
            }
            InitAdditionalProperties(proxy);
        }

        private void InitAdditionalProperties(HttpWebClientProtocol proxy)
        {
            if (proxyTypeHandlers == null)
            {
                proxyTypeHandlers = new Hashtable();
                CustomHandler[] handlerArray1 = Configuration.MasterConfig.ProxyProperties;
                if ((handlerArray1 != null) && (handlerArray1.Length > 0))
                {
                    foreach (CustomHandler handler1 in handlerArray1)
                    {
                        string text1 = handler1.TypeName;
                        string text2 = handler1.Handler;
                        if (((text1 != null) && (text1.Length != 0)) && ((text2 != null) && (text2.Length != 0)))
                        {
                            Type type1 = Type.GetType(text1);
                            if (type1 == null)
                            {
                                MainForm.ShowMessage(this, MessageType.Warning,
                                                     string.Format(typeNotFoundMessage, text1));
                            }
                            else
                            {
                                Type type2 = Type.GetType(text2);
                                if (type2 == null)
                                {
                                    MainForm.ShowMessage(this, MessageType.Warning,
                                                         string.Format(typeNotFoundMessage, text2));
                                }
                                else
                                {
                                    proxyTypeHandlers.Add(type1, type2);
                                }
                            }
                        }
                    }
                }
            }
            for (Type type3 = proxy.GetType(); type3 != typeof (object); type3 = type3.BaseType)
            {
                Type type4 = proxyTypeHandlers[type3] as Type;
                if (type4 != null)
                {
                    AdditionalProperties = (IAdditionalProperties) Activator.CreateInstance(type4, new object[] {proxy});
                    return;
                }
            }
        }

        private ICredentials ReadCredentials(ICredentials credentials, Uri uri, bool useDefaultCredentials,
                                             string userName, string password)
        {
            if ((credentials != null) && !(credentials is CredentialCache))
            {
                return credentials;
            }
            CredentialCache cache1 = credentials as CredentialCache;
            if (cache1 == null)
            {
                cache1 = new CredentialCache();
            }
            if (useDefaultCredentials)
            {
                cache1.Add(uri, "NTLM", (NetworkCredential) CredentialCache.DefaultCredentials);
            }
            else
            {
                cache1.Remove(uri, "NTLM");
            }
            if ((((userName != null) && (userName.Length > 0)) || ((password != null) && (password.Length > 0))) &&
                (cache1.GetCredential(uri, "Basic") == null))
            {
                NetworkCredential credential1 = new NetworkCredential("", "");
                cache1.Add(uri, "Basic", credential1);
            }
            return cache1;
        }

        private void SetCredentialValues(ICredentials credentials, Uri uri, out bool useDefaultCredentials,
                                         out string userName, out string password)
        {
            useDefaultCredentials = false;
            userName = "";
            password = "";
            if (((credentials == null) || (credentials is CredentialCache)) && (credentials != null))
            {
                NetworkCredential credential1 = null;
                CredentialCache cache1 = credentials as CredentialCache;
                if (cache1 != null)
                {
                    if (CredentialCache.DefaultCredentials == cache1.GetCredential(uri, "NTLM"))
                    {
                        useDefaultCredentials = true;
                    }
                    credential1 = cache1.GetCredential(uri, "Basic");
                }
                else if (credentials == CredentialCache.DefaultCredentials)
                {
                    useDefaultCredentials = true;
                }
                else
                {
                    credential1 = credentials as NetworkCredential;
                }
                if (credential1 != null)
                {
                    userName = credential1.UserName;
                    password = credential1.Password;
                }
            }
        }

        public void UpdateProxy(HttpWebClientProtocol proxy)
        {
            proxy.Timeout = Timeout;
            proxy.AllowAutoRedirect = AllowAutoRedirect;
            proxy.PreAuthenticate = PreAuthenticate;
            if (UseCookieContainer)
            {
                if (proxy.CookieContainer == null)
                {
                    proxy.CookieContainer = new CookieContainer();
                }
            }
            else
            {
                proxy.CookieContainer = null;
            }
            proxy.Url = Server.Url;
            proxy.Credentials =
                ReadCredentials(proxy.Credentials, new Uri(Server.Url), Server.UseDefaultCredentials,
                                Server.UserNameForBasicAuth, Server.PasswordForBasicAuth);
            if (((HttpProxy != null) && (HttpProxy.Url != null)) && (HttpProxy.Url.Length > 0))
            {
                Uri uri1 = new Uri(HttpProxy.Url);
                if (proxy.Proxy == null)
                {
                    proxy.Proxy = new WebProxy();
                }
                WebProxy proxy1 = proxy.Proxy as WebProxy;
                proxy1.Address = uri1;
                proxy1.Credentials =
                    ReadCredentials(proxy1.Credentials, uri1, Server.UseDefaultCredentials, Server.UserNameForBasicAuth,
                                    Server.PasswordForBasicAuth);
            }
            if (additionalProperties != null)
            {
                additionalProperties.UpdateProxy(proxy);
            }
        }


        public IAdditionalProperties AdditionalProperties
        {
            get { return additionalProperties; }
            set { additionalProperties = value; }
        }

        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        public ServerProperties HttpProxy
        {
            get { return httpProxy; }
            set { httpProxy = value; }
        }

        public bool PreAuthenticate
        {
            get { return preAuthenticate; }
            set { preAuthenticate = value; }
        }

        public ServerProperties Server
        {
            get { return server; }
            set { server = value; }
        }

        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        public bool UseCookieContainer
        {
            get { return useCookieContainer; }
            set { useCookieContainer = value; }
        }


        private IAdditionalProperties additionalProperties;
        private bool allowAutoRedirect;
        private ServerProperties httpProxy;
        private bool preAuthenticate;
        private static Hashtable proxyTypeHandlers;
        private ServerProperties server;
        private int timeout;
        private static string typeNotFoundMessage;
        private bool useCookieContainer;


        public class ServerProperties
        {
            public string PasswordForBasicAuth;
            public string Url;
            public bool UseDefaultCredentials;
            public string UserNameForBasicAuth;
        }
    }
}
