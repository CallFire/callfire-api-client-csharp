using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Webhooks.Model.Request
{
    public class FindWebhooksRequest : FindRequest
    {
        public string Name { get; set; }

        public ResourceType? Resource { get; set; }

        public ResourceEvent? Event { get; set; }

        public string Callback { get; set; }

        public bool? Enabled { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [FindWebhooksRequest: Name={1}, Resource={2}, Event={3}, Callback={4}, Enabled={5}]",
                base.ToString(), Name, Resource, Event, Callback, Enabled);
        }
    }
}

