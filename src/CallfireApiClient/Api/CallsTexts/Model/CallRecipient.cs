using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class CallRecipient : Recipient
    { 
        public string liveMessage { get; set; }
        public long? liveMessageSoundId { get; set; }
        public string machineMessage { get; set; }
        public long? machineMessageSoundId { get; set; }
        public Voice? voice { get; set; }

        public override string ToString()
        {
            return string.Format("[CallRecipient: {0}, liveMessage={1}, liveMessageSoundId={2}, machineMessage={3}, machineMessageSoundId={4}, voice={5}]", base.ToString(),
                liveMessage, liveMessageSoundId, machineMessage, machineMessageSoundId, voice);
        }
    }
}

