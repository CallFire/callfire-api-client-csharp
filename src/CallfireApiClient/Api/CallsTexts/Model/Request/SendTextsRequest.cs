using Newtonsoft.Json;
using System.Collections.Generic;

namespace  CallfireApiClient.Api.CallsTexts.Model.Request
{
    /// <summary>
    /// Contains fields to send texts (Recipients, DefaultMessage etc)
    /// </summary>
    public class SendTextsRequest : SendCallsTextsRequest
    {
        [JsonIgnore]
        public List<TextRecipient> Recipients;

        public string DefaultMessage { get; set; }

        public SendTextsRequest()
        {
            Recipients = new List<TextRecipient>();
        }

        public override string ToString()
        {
            return string.Format("[SendCallsRequest: {0}, Recipients={1}, DefaultMessage={2}]",
                base.ToString(), Recipients.ToPrettyString(), DefaultMessage);
        }
    }
}
