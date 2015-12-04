﻿using System;
using RestSharp.Authenticators;
using CallfireApiClient.Api.Account;
using CallfireApiClient.Api.Webhooks;
using CallfireApiClient.Api.Contacts;
using CallfireApiClient.Api.Numbers;
using CallfireApiClient.Api.Keywords;
using CallfireApiClient.Api.CallsTexts;
using CallfireApiClient.Api.Campaigns;

namespace CallfireApiClient
{
    public class CallfireClient
    {
        public RestApiClient RestApiClient { get; set; }

        readonly Lazy<MeApi> _MeApi;
        readonly Lazy<OrdersApi> _OrdersApi;
        readonly Lazy<BatchesApi> _BatchesApi;
        readonly Lazy<ContactsApi> _ContactsApi;
        readonly Lazy<ContactListsApi> _ContactListsApi;
        readonly Lazy<NumbersApi> _NumbersApi;
        readonly Lazy<NumberLeasesApi> _NumberLeasesApi;
        readonly Lazy<KeywordsApi> _KeywordsApi;
        readonly Lazy<KeywordLeasesApi> _KeywordLeasesApi;
        readonly Lazy<DncApi> _DncApi;
        readonly Lazy<DncListsApi> _DncListsApi;
        readonly Lazy<CallsApi> _CallsApi;
        readonly Lazy<TextsApi> _TextsApi;

        readonly Lazy<WebhooksApi> _WebhooksApi;

        public MeApi MeApi { get { return _MeApi.Value; } }

        public OrdersApi OrdersApi { get { return _OrdersApi.Value; } }

        public BatchesApi BatchesApi { get { return _BatchesApi.Value; } }

        public ContactsApi ContactsApi { get { return _ContactsApi.Value; } }

        public ContactListsApi ContactListsApi { get { return _ContactListsApi.Value; } }

        public NumbersApi NumbersApi { get { return _NumbersApi.Value; } }

        public NumberLeasesApi NumberLeasesApi { get { return _NumberLeasesApi.Value; } }

        public WebhooksApi WebhooksApi { get { return _WebhooksApi.Value; } }

        public KeywordsApi KeywordsApi { get { return _KeywordsApi.Value; } }

        public KeywordLeasesApi KeywordLeasesApi { get { return _KeywordLeasesApi.Value; } }

        public DncApi DncApi { get { return _DncApi.Value; } }

        public DncListsApi DncListsApi { get { return _DncListsApi.Value; } }

        public CallsApi CallsApi { get { return _CallsApi.Value; } }

        public TextsApi TextsApi { get { return _TextsApi.Value; } }


        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));

            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
            _BatchesApi = new Lazy<BatchesApi>(() => new BatchesApi(RestApiClient));
            _ContactsApi = new Lazy<ContactsApi>(() => new ContactsApi(RestApiClient));
            _ContactListsApi = new Lazy<ContactListsApi>(() => new ContactListsApi(RestApiClient));
            _NumbersApi = new Lazy<NumbersApi>(() => new NumbersApi(RestApiClient));
            _NumberLeasesApi = new Lazy<NumberLeasesApi>(() => new NumberLeasesApi(RestApiClient));
            _WebhooksApi = new Lazy<WebhooksApi>(() => new WebhooksApi(RestApiClient));
            _KeywordsApi = new Lazy<KeywordsApi>(() => new KeywordsApi(RestApiClient));
            _KeywordLeasesApi = new Lazy<KeywordLeasesApi>(() => new KeywordLeasesApi(RestApiClient));
            _DncApi = new Lazy<DncApi>(() => new DncApi(RestApiClient));
            _DncListsApi = new Lazy<DncListsApi>(() => new DncListsApi(RestApiClient));
            _CallsApi = new Lazy<CallsApi>(() => new CallsApi(RestApiClient));
            _TextsApi = new Lazy<TextsApi>(() => new TextsApi(RestApiClient));
        }
    }
}
