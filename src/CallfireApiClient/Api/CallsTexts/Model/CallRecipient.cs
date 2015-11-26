using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class CallRecipient : Recipient
    { 
        public string LiveMessage { get; set; }
        public long? LiveMessageSoundId { get; set; }
        public string MachineMessage { get; set; }
        public long? MachineMessageSoundId { get; set; }
        public Voice? Voice { get; set; }

        public override string ToString()
        {
            return string.Format("[CallRecipient: {0}, liveMessage={1}, liveMessageSoundId={2}, machineMessage={3}, machineMessageSoundId={4}, voice={5}]", base.ToString(),
                LiveMessage, LiveMessageSoundId, MachineMessage, MachineMessageSoundId, Voice);
        }
    }
}

