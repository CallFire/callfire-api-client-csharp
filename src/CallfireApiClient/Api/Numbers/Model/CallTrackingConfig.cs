using System;
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

        public override string ToString()
        {
            return string.Format("[CallTrackingConfig: Screen={0}, Recorded={1}, IntroSoundId={2}, WhisperSoundId={3}, TransferNumbers={4}]",
                Screen, Recorded, IntroSoundId, WhisperSoundId, TransferNumbers);
        }
    }
}

