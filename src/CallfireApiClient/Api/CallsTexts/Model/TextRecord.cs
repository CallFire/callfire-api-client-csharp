using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class TextRecord : ActionRecord
    {
        public string Message { get; set; }

        [JsonProperty("textResult")]
        public TextResult? Result { get; set; }

        public enum TextResult
        {
            SENT,
            RECEIVED,
            DNT,
            TOO_BIG,
            INTERNAL_ERROR,
            CARRIER_ERROR,
            CARRIER_TEMP_ERROR,
            UNDIALED,
        }

        public override string ToString()
        {
            return string.Format("{0} [TextRecord: Message={1}, Result={2}]", base.ToString(), Message, Result);
        }
    }
}

