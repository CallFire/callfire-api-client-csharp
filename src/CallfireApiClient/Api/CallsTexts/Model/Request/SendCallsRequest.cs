

using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Campaigns.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace  CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Contains fields to send calls (recipients, campaignId etc)
    /// </summary>
    public class SendCallsRequest : CallfireModel
    {
        [JsonIgnore]
        public List<CallRecipient> Recipients;

        public long? CampaignId { get; set; }

        public string DefaultLiveMessage { get; set; }

        public string DefaultMachineMessage { get; set; }

        public long? DefaultLiveMessageSoundId { get; set; }

        public long? DefaultMachineMessageSoundId { get; set; }

        public Voice DefaultVoice { get; set; }

        public string Fields { get; set; }

        public SendCallsRequest()
        {
            Recipients = new List<CallRecipient>();
        }

        public override string ToString()
        {
            return string.Format("[SendCallsRequest: Recipients={0}, CampaignId={1}, DefaultLiveMessage={2}, DefaultMachineMessage={3}, DefaultLiveMessageSoundId={4}, DefaultMachineMessageSoundId ={5}, DefaultVoice ={6}, Fields ={7}]",
                Recipients, CampaignId, DefaultLiveMessage, DefaultMachineMessage, DefaultLiveMessageSoundId, DefaultMachineMessageSoundId, DefaultVoice, Fields);
        }
    }
}
