using System;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CallBroadcast : Broadcast
    {
        public RetryConfig RetryConfig { get; set; }

        public IList<Recipient> Recipients { get; set; }

        /**
         * IVR xml document describing dialplan. If dialplanXml != null then this is IVR broadcast
         */
        public string DialplanXml { get; set; }

        public CallBroadcastSounds Sounds { get; set; }

        public AnsweringMachineConfig? AnsweringMachineConfig { get; set; }

        public int? MaxActiveTransfers { get; set; }

        public override string ToString()
        {
            return string.Format("[CallBroadcast: RetryConfig={0}, Recipients={1}, DialplanXml={2}, Sounds={3}, AnsweringMachineConfig={4}, MaxActiveTransfers={5}]",
                RetryConfig, Recipients, DialplanXml, Sounds, AnsweringMachineConfig, MaxActiveTransfers);
        }
    }
}

