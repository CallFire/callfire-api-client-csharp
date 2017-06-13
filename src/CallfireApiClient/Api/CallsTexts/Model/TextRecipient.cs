using CallfireApiClient.Api.Campaigns.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class TextRecipient : Recipient
    {
        public string Message { get; set; }

        public IList<Media> Media { get; set; }

        public override string ToString()
        {
            return string.Format("[TextRecipient: {0}, Message={1}, Media={2}]", base.ToString(), Message, Media.ToPrettyString());
        }
    }
}