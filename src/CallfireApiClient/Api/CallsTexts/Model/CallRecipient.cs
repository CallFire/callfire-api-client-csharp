using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class CallRecipient : Recipient
    { 
        public string LiveMessage { get; set; }
        public long? LiveMessageSoundId { get; set; }
        public string MachineMessage { get; set; }
        public long? MachineMessageSoundId { get; set; }
        public string TransferMessage { get; set; }
        public long? TransferMessageSoundId { get; set; }
        public string TransferDigit { get; set; }
        public string TransferNumber { get; set; }
        public Voice? Voice { get; set; }
        public string DialplanXml { get; set; }

        public override string ToString()
        {
            return string.Format("[CallRecipient: {0}, liveMessage={1}, liveMessageSoundId={2}, machineMessage={3}, machineMessageSoundId={4}, transferMessage={5}, transferMessageSoundId={6}, transferDigit={7}, transferNumber={8}, voice={9}, dialplanXml={10}]", base.ToString(),
                LiveMessage, LiveMessageSoundId, MachineMessage, MachineMessageSoundId, TransferMessage, TransferMessageSoundId, TransferDigit, TransferNumber, Voice, DialplanXml);
        }
    }
}

