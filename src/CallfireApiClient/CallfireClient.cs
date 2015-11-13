using System;
using RestSharp.Authenticators;
using CallfireApiClient.Api.Account;
using System.Diagnostics;
using CallfireApiClient.Api.Webhooks;
using CallfireApiClient.Api.Contacts;
using CallfireApiClient.Api.Numbers;

namespace CallfireApiClient
{
    public class CallfireClient
    {
        public RestApiClient RestApiClient { get; set; }

        readonly Lazy<MeApi> _MeApi;
        readonly Lazy<OrdersApi> _OrdersApi;
        readonly Lazy<ContactsApi> _ContactsApi;
        readonly Lazy<NumbersApi> _NumbersApi;
        readonly Lazy<NumberLeasesApi> _NumberLeasesApi;

        readonly Lazy<WebhooksApi> _WebhooksApi;

        public MeApi MeApi { get { return _MeApi.Value; } }

        public OrdersApi OrdersApi { get { return _OrdersApi.Value; } }

        public ContactsApi ContactsApi { get { return _ContactsApi.Value; } }

        public NumbersApi NumbersApi { get { return _NumbersApi.Value; } }

        public NumberLeasesApi NumberLeasesApi { get { return _NumberLeasesApi.Value; } }

        public WebhooksApi WebhooksApi { get { return _WebhooksApi.Value; } }

        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));

            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
            _ContactsApi = new Lazy<ContactsApi>(() => new ContactsApi(RestApiClient));
            _NumbersApi = new Lazy<NumbersApi>(() => new NumbersApi(RestApiClient));
            _NumberLeasesApi = new Lazy<NumberLeasesApi>(() => new NumberLeasesApi(RestApiClient));
            _WebhooksApi = new Lazy<WebhooksApi>(() => new WebhooksApi(RestApiClient));
        }
    }
}

