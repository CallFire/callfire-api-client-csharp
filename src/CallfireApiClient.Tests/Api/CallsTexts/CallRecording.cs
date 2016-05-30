using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Tests.Api.CallsTexts
{
    internal class CallRecording<T>
    {
        private IList<CallRecording> callRecordings;

        public CallRecording(IList<CallRecording> callRecordings)
        {
            this.callRecordings = callRecordings;
        }
    }
}