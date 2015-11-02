using System;
using RestSharp.Authenticators;
using CallfireApiClient.Api.Account;

namespace CallfireApiClient
{
    public class CallfireClient
    {
        readonly RestApiClient RestApiClient;

        readonly Lazy<MeApi> _MeApi;
        readonly Lazy<OrdersApi> _OrdersApi;

        public MeApi MeApi
        { 
            get
            {
                return _MeApi.Value;
            }
        }

        public OrdersApi OrdersApi
        { 
            get
            {
                return _OrdersApi.Value;
            }
        }

        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));
            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
        }
    }
}

