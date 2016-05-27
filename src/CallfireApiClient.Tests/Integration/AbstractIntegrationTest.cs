using CallfireApiClient;

namespace CallfireApiClient.Tests.Integration
{
    public class AbstractIntegrationTest
    {
        protected CallfireClient Client;

        public AbstractIntegrationTest()
        {
            Client = new CallfireClient("9b4f74b51316", "608bec4e28889510");
        }
    }
}

