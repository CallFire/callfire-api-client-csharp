using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public class Subscription : CallfireModel
    {
        public long? Id { get; set; }

        public bool? Enabled { get; set; }

        public string Endpoint { get; set; }

        public bool? NonStrictSsl { get; set; }

        public NotificationFormat? NotificationFormat { get; set; }

        public TriggerEvent? TriggerEvent { get; set; }

        public long? BroadcastId { get; set; }

        public long? BatchId { get; set; }

        public string FromNumber { get; set; }

        public string ToNumber { get; set; }

        public override string ToString()
        {
            return string.Format("[Subscription: Id={0}, Enabled={1}, Endpoint={2}, NonStrictSsl={3}, NotificationFormat={4}, TriggerEvent={5}, BroadcastId={6}, BatchId={7}, FromNumber={8}, ToNumber={9}]",
                Id, Enabled, Endpoint, NonStrictSsl, NotificationFormat, TriggerEvent, BroadcastId, BatchId, FromNumber, ToNumber);
        }
    }
}

