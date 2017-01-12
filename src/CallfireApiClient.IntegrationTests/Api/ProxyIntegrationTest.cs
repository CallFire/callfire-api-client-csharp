using System;
using NUnit.Framework;

namespace CallfireApiClient.IntegrationTests.Api
{
    [TestFixture, Ignore]
    public class ProxyIntegrationTest
    {
        [Test]
        public void QueryCallfireThroughProxyWithBasicAuth()
        {
            RestApiClient.getClientConfig().Add(ClientConstants.PROXY_ADDRESS_PROPERTY, "localhost:3128");
            RestApiClient.getClientConfig().Add(ClientConstants.PROXY_CREDENTIALS_PROPERTY, "proxyuser:proxypass");
            CallfireClient Client = new CallfireClient("", "");
            var account = Client.MeApi.GetAccount();
            Console.WriteLine("account: " + account);
        }

        [Test]
        public void QueryCallfireThroughProxyWithProxyAuth()
        {
            ProxyAuthenticator auth = new ProxyAuthenticator()
            {
                ProxyAddress = "localhost:3128",
                ProxyCredentials = "proxyuser:proxypass"
            };

            CallfireClient Client = new CallfireClient(auth);
            var account = Client.MeApi.GetAccount();
            Console.WriteLine("account: " + account);
        }
    }
}
