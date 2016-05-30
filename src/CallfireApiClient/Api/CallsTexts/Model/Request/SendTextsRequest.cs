

using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Campaigns.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace  CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Contains fields to send texts (recipients, campaignId etc)
    /// </summary>
    public class SendTextsRequest : CallfireModel
    {
        [JsonIgnore]
        public List<TextRecipient> Recipients;

        public long? CampaignId { get; set; }

        public string DefaultMessage { get; set; }

        public string Fields { get; set; }

        public SendTextsRequest()
        {
            Recipients = new List<TextRecipient>();
        }

        public override string ToString()
        {
            return string.Format("[SendCallsRequest: Recipients={0}, CampaignId={1}, DefaultMessage={2}, Fields ={3}]",
                Recipients, CampaignId, DefaultMessage, Fields);
        }
    }
}
