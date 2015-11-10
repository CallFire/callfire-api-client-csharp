using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Webhooks.Model.Request
{
    public class FindWebhooksRequest : FindRequest
    {
        public string Name { get; set; }

        public string Resource { get; set; }

        public string Event { get; set; }

        public string Callback { get; set; }

        public bool? Enabled { get; set; }
    }
}

