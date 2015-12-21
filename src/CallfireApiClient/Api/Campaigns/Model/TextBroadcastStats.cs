using System;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class TextBroadcastStats : BroadcastStats
    {
        public int? SentCount { get; private set; }

        public int? UnsentCount { get; private set; }
        // property with typo
        [JsonProperty("recievedCount")]
        public int? ReceivedCount { get; private set; }

        public int? DoNotTextCount { get; private set; }

        public int? TooBigCount { get; private set; }

        public int? ErrorCount { get; private set; }

        public override string ToString()
        {
            return string.Format("[{0} TextBroadcastStats: SentCount={1}, UnsentCount={2}, ReceivedCount={3}, DoNotTextCount={4}, TooBigCount={5}, ErrorCount={6}]",
                base.ToString(), SentCount, UnsentCount, ReceivedCount, DoNotTextCount, TooBigCount, ErrorCount);
        }
    }
}

