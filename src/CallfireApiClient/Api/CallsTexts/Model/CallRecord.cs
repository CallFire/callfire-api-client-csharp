﻿using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class CallRecord : ActionRecord
    {
        public CallResult? Result { get; private set; }

        public IList<Note> Notes { get; set; }

        public IList<CallRecording> Recordings { get; private set; }

        public IList<QuestionResponse> QuestionResponses { get; private set; }

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
            return string.Format("{0} [CallRecord: Result={1}, Notes={2}, Recordings={3}]", base.ToString(),
                Result, Notes?.ToPrettyString(), Recordings?.ToPrettyString());
        }
    }
}

