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
            RestApiClient.getApplicationConfig().Add(ClientConstants.PROXY_ADDRESS_PROPERTY, "localhost:3128");
            RestApiClient.getApplicationConfig().Add(ClientConstants.PROXY_CREDENTIALS_PROPERTY, "proxyuser:proxypass");
            CallfireClient Client = new CallfireClient("", "");
            var account = Client.MeApi.GetAccount();
            Console.WriteLine("account: " + account);
        }

        [Test]
        public void QueryCallfireThroughProxyWithProxy()
        {
            CallfireClient Client = new CallfireClient("", "");

            Client.SetClientConfig(new ClientConfig(){
                                        ApiBasePath = "https://api.callfire.com/v2",
                                        ProxyAddress = "localhost",
                                        ProxyPort = 3128,
                                        ProxyLogin = "proxyuser",
                                        ProxyPassword = "proxypass"
                                    });

            var account = Client.MeApi.GetAccount();
            Console.WriteLine("account: " + account);
        }
    }
}
