using System;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public enum RetryResults
    {
        LA,
        AM,
        BUSY,
        DNC,
        XFER,
        XFER_LEG,
        NO_ANS,
        UNDIALED,
        SENT,
        RECEIVED,
        DNT,
        TOO_BIG,
        INTERNAL_ERROR,
        CARRIER_ERROR,
        CARRIER_TEMP_ERROR,
        SD,
        POSTPONED,
        ABANDONED,
        SKIPPED
    }
}

