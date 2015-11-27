using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindTextsRequest : FindCallsTextsRequest
    {
        public List<Text.StateType> States { get; set; }

        public List<TextRecord.TextResult> Results { get; set; }

        public FindTextsRequest()
        {
            States = new List<Text.StateType>();
            Results = new List<TextRecord.TextResult>();
        }

        public override string ToString()
        {
            return string.Format("[FindCallsRequest: {0}, states={1}, results={2}]", States.ToPrettyString(), Results.ToPrettyString());
        }
    }
}

