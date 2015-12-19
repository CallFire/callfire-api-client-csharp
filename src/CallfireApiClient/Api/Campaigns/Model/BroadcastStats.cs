using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class BroadcastStats : CallfireModel
    {
        public int? TotalOutboundCount { get; private set; }

        public int? remainingOutboundCount{ get; private set; }

        public Decimal billedAmount{ get; private set; }

        public override string ToString()
        {
            return string.Format("[BroadcastStats: TotalOutboundCount={0}, remainingOutboundCount={1}, billedAmount={2}]",
                TotalOutboundCount, remainingOutboundCount, billedAmount);
        }
    }
}

