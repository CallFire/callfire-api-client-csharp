using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CallBroadcastSounds : CallfireModel
    {
        public string LiveSoundText { get; set; }

        public Voice? LiveSoundTextVoice { get; set; }

        public long? LiveSoundId { get; set; }

        public string MachineSoundText { get; set; }

        public Voice? MachineSoundTextVoice { get; set; }

        public long? MachineSoundId { get; set; }

        public string TransferSoundText { get; set; }

        public Voice? TransferSoundTextVoice { get; set; }

        public long? TransferSoundId { get; set; }

        public string TransferDigit { get; set; }

        public string TransferNumber { get; set; }

        public string DncSoundText { get; set; }

        public Voice? DncSoundTextVoice { get; set; }

        public long? DncSoundId { get; set; }

        public string DncDigit { get; set; }

        public override string ToString()
        {
            return string.Format("[CallBroadcastSounds: LiveSoundText={0}, LiveSoundTextVoice={1}, LiveSoundId={2}, MachineSoundText={3}, MachineSoundTextVoice={4}, MachineSoundId={5}, TransferSoundText={6}, TransferSoundTextVoice={7}, TransferSoundId={8}, TransferDigit={9}, TransferNumber={10}, DncSoundText={11}, DncSoundTextVoice={12}, DncSoundId={13}, DncDigit={14}]",
                LiveSoundText, LiveSoundTextVoice, LiveSoundId, MachineSoundText, MachineSoundTextVoice, MachineSoundId, TransferSoundText, TransferSoundTextVoice, TransferSoundId, TransferDigit, TransferNumber, DncSoundText, DncSoundTextVoice, DncSoundId, DncDigit);
        }
    }

}

