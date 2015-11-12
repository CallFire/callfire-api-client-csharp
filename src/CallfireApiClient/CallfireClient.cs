using System;
using RestSharp.Authenticators;
using CallfireApiClient.Api.Account;
using System.Diagnostics;
using CallfireApiClient.Api.Webhooks;
using CallfireApiClient.Api.Contacts;

namespace CallfireApiClient
{
    public class CallfireClient
    {
        public RestApiClient RestApiClient { get; set; }

        readonly Lazy<MeApi> _MeApi;
        readonly Lazy<OrdersApi> _OrdersApi;
        readonly Lazy<ContactsApi> _ContactsApi;

        readonly Lazy<WebhooksApi> _WebhooksApi;

        public MeApi MeApi { get { return _MeApi.Value; } }

        public OrdersApi OrdersApi { get { return _OrdersApi.Value; } }

        public ContactsApi ContactsApi { get { return _ContactsApi.Value; } }

        public WebhooksApi WebhooksApi { get { return _WebhooksApi.Value; } }

        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));

            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
            _ContactsApi = new Lazy<ContactsApi>(() => new ContactsApi(RestApiClient));
            _WebhooksApi = new Lazy<WebhooksApi>(() => new WebhooksApi(RestApiClient));
        }
    }
}

