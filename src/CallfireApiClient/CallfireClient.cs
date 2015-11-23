﻿using System;
using RestSharp.Authenticators;
using CallfireApiClient.Api.Account;
using CallfireApiClient.Api.Webhooks;
using CallfireApiClient.Api.Contacts;
using CallfireApiClient.Api.Numbers;
using CallfireApiClient.Api.Keywords;

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
        readonly Lazy<KeywordsApi> _KeywordsApi;
        readonly Lazy<KeywordLeasesApi> _KeywordLeasesApi;
        readonly Lazy<DncApi> _DncApi;
        readonly Lazy<DncListsApi> _DncListsApi;

        readonly Lazy<WebhooksApi> _WebhooksApi;

        public MeApi MeApi { get { return _MeApi.Value; } }

        public OrdersApi OrdersApi { get { return _OrdersApi.Value; } }

        public ContactsApi ContactsApi { get { return _ContactsApi.Value; } }

        public NumbersApi NumbersApi { get { return _NumbersApi.Value; } }

        public NumberLeasesApi NumberLeasesApi { get { return _NumberLeasesApi.Value; } }

        public WebhooksApi WebhooksApi { get { return _WebhooksApi.Value; } }

        public KeywordsApi KeywordsApi { get { return _KeywordsApi.Value; } }
        public KeywordLeasesApi KeywordLeasesApi { get { return _KeywordLeasesApi.Value; } }

        public DncApi DncApi { get { return _DncApi.Value; } }
        public DncListsApi DncListsApi { get { return _DncListsApi.Value; } }


        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));

            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
            _ContactsApi = new Lazy<ContactsApi>(() => new ContactsApi(RestApiClient));
            _NumbersApi = new Lazy<NumbersApi>(() => new NumbersApi(RestApiClient));
            _NumberLeasesApi = new Lazy<NumberLeasesApi>(() => new NumberLeasesApi(RestApiClient));
            _WebhooksApi = new Lazy<WebhooksApi>(() => new WebhooksApi(RestApiClient));
            _KeywordsApi = new Lazy<KeywordsApi>(() => new KeywordsApi(RestApiClient));
            _KeywordLeasesApi = new Lazy<KeywordLeasesApi>(() => new KeywordLeasesApi(RestApiClient));
            _DncApi = new Lazy<DncApi>(() => new DncApi(RestApiClient));
            _DncListsApi = new Lazy<DncListsApi>(() => new DncListsApi(RestApiClient));
        }
    }
}
