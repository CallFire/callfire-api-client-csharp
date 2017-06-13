using CallfireApiClient.Api.Campaigns.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace  CallfireApiClient.Api.CallsTexts.Model.Request
{
    /// <summary>
    /// Contains fields to send calls (Recipients, DefaultVoice etc)
    /// </summary>
    public class SendCallsRequest : SendCallsTextsRequest
    {
        [JsonIgnore]
        public List<CallRecipient> Recipients;

        public string DefaultLiveMessage { get; set; }

        public string DefaultMachineMessage { get; set; }

        public long? DefaultLiveMessageSoundId { get; set; }

        public long? DefaultMachineMessageSoundId { get; set; }

        public Voice DefaultVoice { get; set; }

        public SendCallsRequest()
        {
            Recipients = new List<CallRecipient>();
        }

        public override string ToString()
        {
            return string.Format("[SendCallsRequest: {0}, Recipients={1}, DefaultLiveMessage={2}, DefaultMachineMessage={3}, DefaultLiveMessageSoundId={4}, DefaultMachineMessageSoundId ={5}, DefaultVoice ={6}]",
                base.ToString(), Recipients.ToPrettyString(), DefaultLiveMessage, DefaultMachineMessage, DefaultLiveMessageSoundId, DefaultMachineMessageSoundId, DefaultVoice);
        }
    }
}
