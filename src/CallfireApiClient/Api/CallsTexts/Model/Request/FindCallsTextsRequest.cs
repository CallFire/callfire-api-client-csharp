using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindCallsTextsRequest : FindRequest
    {
        public long? CampaignId { get; set; }

        public long? BatchId { get; set; }

        public string FromNumber { get; set; }

        public string ToNumber { get; set; }

        public string Label { get; set; }

        public bool? Inbound { get; set; }

        public DateTime? IntervalBegin { get; set; }

        public DateTime? IntervalEnd { get; set; }

        public List<long> Id { get; set; }

        public List<StateType> States { get; set; }

        public FindCallsTextsRequest()
        {
            States = new List<StateType>();
        }

        public override string ToString()
        {
            return string.Format("[FindCallsTextsRequest: {0}, campaignId={1}, fromNumber={2}, toNumber={3}, label={4}, inbound={5}, intervalBegin={6}, intervalEnd={7}], id={8} states={9} batchId={10}]]",
                base.ToString(), CampaignId, FromNumber, ToNumber, Label, Inbound, IntervalBegin, IntervalEnd, Id, States.ToPrettyString(), BatchId);
        }
    }
}

