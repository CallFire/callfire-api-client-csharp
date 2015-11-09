using System;

namespace CallfireApiClient.Api.Account.Model
{
    public enum Status
    {
        NEW,
        PROCESSING,
        FINISHED,
        ERRORED,
        VOID,
        WAIT_FOR_PAYMENT,
        ADJUSTED,
        APPROVE_TIER_ONE,
        APPROVE_TIER_TWO,
        REJECTED
    }
}

