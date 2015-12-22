using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    /// <summary>
    /// Holds general statistic fields for Text and Call broadcasts
    /// </summary>
    public abstract class BroadcastStats : CallfireModel
    {
        public int? TotalOutboundCount { get; private set; }

        public int? RemainingOutboundCount { get; private set; }

        public Decimal? BilledAmount { get; private set; }

        public override string ToString()
        {
            return string.Format("[BroadcastStats: TotalOutboundCount={0}, RemainingOutboundCount={1}, BilledAmount={2}]",
                TotalOutboundCount, RemainingOutboundCount, BilledAmount);
        }
    }
}

