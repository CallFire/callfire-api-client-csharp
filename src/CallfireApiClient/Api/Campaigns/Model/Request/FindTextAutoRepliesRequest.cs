using System;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Campaigns.Model.Request
{
    public class FindTextAutoRepliesRequest : FindRequest
    {
        public string Number { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} FindTextAutoRepliesRequest: Number={1}]", base.ToString(), Number);
        }
    }
}

