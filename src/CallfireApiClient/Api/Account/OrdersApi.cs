using System;

namespace CallfireApiClient.Api.Account
{
    public class OrdersApi
    {
        readonly RestApiClient Client;

        internal OrdersApi(RestApiClient client)
        {
            Client = client;
        }
    }
}

