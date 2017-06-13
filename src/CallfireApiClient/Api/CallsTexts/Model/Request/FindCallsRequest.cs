using System.Collections.Generic;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindCallsRequest : FindCallsTextsRequest
    {
        public List<Call.CallResult> Results { get; set; }

        public FindCallsRequest()
        {
            Results = new List<Call.CallResult>();
        }

        public override string ToString()
        {
            return string.Format("[FindCallsRequest: {0}, results={1}]", base.ToString(), Results.ToPrettyString());
        }
    }
}

