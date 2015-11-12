using System;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class Text : Action<TextRecord>
    {
        public string Message { get; set; }

        public TextResult FinalTextResult { get; set; }

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
            return string.Format("{0} [Text: Message={1}, FinalTextResult={2}]", base.ToString(), Message, FinalTextResult);
        }
    }
}

