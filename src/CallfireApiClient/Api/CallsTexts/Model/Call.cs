using System;
using System.Collections.Generic;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class Call : Action<CallRecord>
    {
        public CallResult? FinalCallResult { get; private set; }

        public bool? AgentCall { get; private set; }

        public IList<Note> Notes { get; set; }

        public enum CallResult
        {
            LA,
            AM,
            BUSY,
            DNC,
            XFER,
            NO_ANS,
            XFER_LEG,
            INTERNAL_ERROR,
            CARRIER_ERROR,
            CARRIER_TEMP_ERROR,
            UNDIALED,
            SD,
            POSTPONED,
            ABANDONED,
            SKIPPED,
        }

        public override string ToString()
        {
            return string.Format("{0} [Call: FinalCallResult={1}, AgentCall={2}, Notes={3}]", base.ToString(),
                FinalCallResult, AgentCall, Notes?.ToPrettyString());
        }
    }
}

