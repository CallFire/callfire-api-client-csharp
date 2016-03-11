using System;
using NUnit.Framework;

namespace CallfireApiClient.Tests.Api
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
    }
}
