using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public class WebhookResource : CallfireModel
    {
        public string Resource { get; private set; }

        public ISet<ResourceEvent> SupportedEvents { get; private set; }

        public override string ToString()
        {
            return string.Format("[WebhookResource: Resource={0}, SupportedEvents={1}]", Resource, SupportedEvents?.ToPrettyString());
        }
    }
}

