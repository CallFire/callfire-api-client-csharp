using CallfireApiClient;

namespace CallfireApiClient.IntegrationTests.Api
{
    public class AbstractIntegrationTest
    {
        protected CallfireClient Client;

        public AbstractIntegrationTest()
        {
            Client = new CallfireClient("login", "password");
        }
    }
}

