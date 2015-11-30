using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindTextsRequest : FindCallsTextsRequest
    {
        public List<TextRecord.TextResult> Results { get; set; }

        public FindTextsRequest()
        {
            Results = new List<TextRecord.TextResult>();
        }

        public override string ToString()
        {
            return string.Format("[FindCallsRequest: {0}, results={1}]", base.ToString(), Results.ToPrettyString());
        }
    }
}

