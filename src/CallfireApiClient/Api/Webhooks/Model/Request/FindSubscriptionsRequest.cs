using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Webhooks.Model.Request
{
    public class FindSubscriptionsRequest : FindRequest
    {
        public long? CampaignId { get; set; }

        public TriggerEvent? Trigger { get; set; }

        public NotificationFormat? Format { get; set; }

        public string FromNumber { get; set; }

        public string ToNumber { get; set; }

        public override string ToString()
        {
            return string.Format("[FindSubscriptionsRequest: CampaignId={0}, Trigger={1}, Format={2}, FromNumber={3}, ToNumber={4}]",
                CampaignId, Trigger, Format, FromNumber, ToNumber);
        }
    }
}

