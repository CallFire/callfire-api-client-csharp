using RestSharp.Authenticators;

namespace CallfireApiClient
{
    /// <summary>
    /// authenticator to include proxy configuration parameters
    /// <summary>
    public class ProxyAuthenticator
    {
        public string ProxyAddress { get; set; }

        public string ProxyCredentials { get; set; }

        public ProxyAuthenticator(string proxyAddress, string proxyCredentials)
        {
            ProxyAddress = proxyAddress;
            ProxyCredentials = proxyCredentials;
        }

        public ProxyAuthenticator()
        {
        }
    }
}