using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindCallsRequest : FindCallsTextsRequest
    {
        public List<Call.StateType> States { get; set; }

        public List<Call.CallResult> Results { get; set; }

        public FindCallsRequest()
        {
            States = new List<Call.StateType>();
            Results = new List<Call.CallResult>();
        }

        public override string ToString()
        {
            return string.Format("[FindCallsRequest: {0}, states={1}, results={2}]", base.ToString(),
                States.ToPrettyString(), Results.ToPrettyString());
        }
    }
}

