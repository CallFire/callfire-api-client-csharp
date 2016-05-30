using CallfireApiClient;

namespace CallfireApiClient.Tests.Integration
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

