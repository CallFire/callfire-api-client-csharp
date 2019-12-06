using System;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CallBroadcast : Broadcast
    {
        public RetryConfig RetryConfig { get; set; }

        public IList<CallRecipient> Recipients { get; set; }

        /**
         * IVR xml document describing dialplan. If dialplanXml != null then this is IVR broadcast
         */
        public string DialplanXml { get; set; }

        public CallBroadcastSounds Sounds { get; set; }

        public AnsweringMachineConfig? AnsweringMachineConfig { get; set; }

        public int? MaxActiveTransfers { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} CallBroadcast: RetryConfig={1}, Recipients={2}, DialplanXml={3}, Sounds={4}, AnsweringMachineConfig={5}, MaxActiveTransfers={6}]",
                base.ToString(), RetryConfig, Recipients, DialplanXml, Sounds, AnsweringMachineConfig, MaxActiveTransfers);
        }
    }
}

