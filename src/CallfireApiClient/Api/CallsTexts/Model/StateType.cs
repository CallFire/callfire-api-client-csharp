using System;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public enum StateType
    {
        // non-terminal
        READY,
        SELECTED,
        CALLBACK,
        // terminal
        FINISHED,
        DISABLED,
        DNC,
        DUP,
        INVALID,
        TIMEOUT,
        PERIOD_LIMIT,
    }
}

