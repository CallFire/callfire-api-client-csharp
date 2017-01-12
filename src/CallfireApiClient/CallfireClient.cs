using System;
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
    /// <summary>
    /// Callfire API v2 .NET client
    /// use your API login and password to create client instance object
    /// </summary>
    public class CallfireClient
    {
        public RestApiClient RestApiClient { get; set; }

        private Lazy<MeApi> _MeApi;
        private Lazy<OrdersApi> _OrdersApi;
        private Lazy<BatchesApi> _BatchesApi;
        private Lazy<CampaignSoundsApi> _CampaignSoundsApi;
        private Lazy<ContactsApi> _ContactsApi;
        private Lazy<ContactListsApi> _ContactListsApi;
        private Lazy<NumbersApi> _NumbersApi;
        private Lazy<NumberLeasesApi> _NumberLeasesApi;
        private Lazy<KeywordsApi> _KeywordsApi;
        private Lazy<KeywordLeasesApi> _KeywordLeasesApi;
        private Lazy<DncApi> _DncApi;
        private Lazy<CallsApi> _CallsApi;
        private Lazy<TextsApi> _TextsApi;
        private Lazy<TextAutoRepliesApi> _TextAutoRepliesApi;
        private Lazy<TextBroadcastsApi> _TextBroadcastsApi;
        private Lazy<CallBroadcastsApi> _CallBroadcastsApi;
        private Lazy<MediaApi> _MediaApi;
        private Lazy<SubscriptionsApi> _SubscriptionsApi;
        private Lazy<WebhooksApi> _WebhooksApi;

        public MeApi MeApi { get { return _MeApi.Value; } }

        public OrdersApi OrdersApi { get { return _OrdersApi.Value; } }

        public BatchesApi BatchesApi { get { return _BatchesApi.Value; } }

        public CampaignSoundsApi CampaignSoundsApi { get { return _CampaignSoundsApi.Value; } }

        public ContactsApi ContactsApi { get { return _ContactsApi.Value; } }

        public ContactListsApi ContactListsApi { get { return _ContactListsApi.Value; } }

        public NumbersApi NumbersApi { get { return _NumbersApi.Value; } }

        public NumberLeasesApi NumberLeasesApi { get { return _NumberLeasesApi.Value; } }

        public SubscriptionsApi SubscriptionsApi { get { return _SubscriptionsApi.Value; } }

        public WebhooksApi WebhooksApi { get { return _WebhooksApi.Value; } }

        public KeywordsApi KeywordsApi { get { return _KeywordsApi.Value; } }

        public KeywordLeasesApi KeywordLeasesApi { get { return _KeywordLeasesApi.Value; } }

        //TODO vmalinovskiy: uncomment when dnc apis will be tested and available on docs site
        //public DncApi DncApi { get { return _DncApi.Value; } }

        public CallsApi CallsApi { get { return _CallsApi.Value; } }

        public TextsApi TextsApi { get { return _TextsApi.Value; } }

        public TextAutoRepliesApi TextAutoRepliesApi { get { return _TextAutoRepliesApi.Value; } }

        public TextBroadcastsApi TextBroadcastsApi { get { return _TextBroadcastsApi.Value; } }

        public CallBroadcastsApi CallBroadcastsApi { get { return _CallBroadcastsApi.Value; } }

        public MediaApi MediaApi { get { return _MediaApi.Value; } }

        public CallfireClient(string username, string password)
        {
            RestApiClient = new RestApiClient(new HttpBasicAuthenticator(username, password));
            InitApis();
        }

        public CallfireClient(ProxyAuthenticator proxyAuth)
        {
            RestApiClient = new RestApiClient(proxyAuth);
            InitApis();
        }

        private void InitApis()
        {
            _MeApi = new Lazy<MeApi>(() => new MeApi(RestApiClient));
            _OrdersApi = new Lazy<OrdersApi>(() => new OrdersApi(RestApiClient));
            _BatchesApi = new Lazy<BatchesApi>(() => new BatchesApi(RestApiClient));
            _CampaignSoundsApi = new Lazy<CampaignSoundsApi>(() => new CampaignSoundsApi(RestApiClient));
            _ContactsApi = new Lazy<ContactsApi>(() => new ContactsApi(RestApiClient));
            _ContactListsApi = new Lazy<ContactListsApi>(() => new ContactListsApi(RestApiClient));
            _NumbersApi = new Lazy<NumbersApi>(() => new NumbersApi(RestApiClient));
            _NumberLeasesApi = new Lazy<NumberLeasesApi>(() => new NumberLeasesApi(RestApiClient));
            _SubscriptionsApi = new Lazy<SubscriptionsApi>(() => new SubscriptionsApi(RestApiClient));
            _WebhooksApi = new Lazy<WebhooksApi>(() => new WebhooksApi(RestApiClient));
            _KeywordsApi = new Lazy<KeywordsApi>(() => new KeywordsApi(RestApiClient));
            _KeywordLeasesApi = new Lazy<KeywordLeasesApi>(() => new KeywordLeasesApi(RestApiClient));

            //TODO vmalinovskiy: uncomment when dnc apis will be tested and available on docs site
            //_DncApi = new Lazy<DncApi>(() => new DncApi(RestApiClient));

            _CallsApi = new Lazy<CallsApi>(() => new CallsApi(RestApiClient));
            _TextsApi = new Lazy<TextsApi>(() => new TextsApi(RestApiClient));
            _TextAutoRepliesApi = new Lazy<TextAutoRepliesApi>(() => new TextAutoRepliesApi(RestApiClient));
            _TextBroadcastsApi = new Lazy<TextBroadcastsApi>(() => new TextBroadcastsApi(RestApiClient));
            _CallBroadcastsApi = new Lazy<CallBroadcastsApi>(() => new CallBroadcastsApi(RestApiClient));
            _MediaApi = new Lazy<MediaApi>(() => new MediaApi(RestApiClient));
        }
    }
}
