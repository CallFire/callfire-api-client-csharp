using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class CallTrackingConfig : CallfireModel
    {
        public bool? Screen { get; set; }

        public bool? Recorded { get; set; }

        public long? IntroSoundId { get; set; }

        public long? WhisperSoundId { get; set; }

        public IList<string> TransferNumbers { get; set; }

        public bool? Voicemail { get; set; }

        public long? VoicemailSoundId { get; set; }

        public long? FailedTransferSoundId { get; set; }

        public WeeklySchedule WeeklySchedule { get; set; }

        public GoogleAnalytics GoogleAnalytics { get; set; }

        public override string ToString()
        {
            return string.Format("[CallTrackingConfig: Screen={0}, Recorded={1}, IntroSoundId={2}, WhisperSoundId={3}, TransferNumbers={4}, Voicemail={5}, VoicemailSoundId={6}, FailedTransferSoundId={7}, WeeklySchedule ={8}, GoogleAnalytics ={9}]",
                Screen, Recorded, IntroSoundId, WhisperSoundId, TransferNumbers, Voicemail, VoicemailSoundId, FailedTransferSoundId, WeeklySchedule, GoogleAnalytics);
        }
    }
}
