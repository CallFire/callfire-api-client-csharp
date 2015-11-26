using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class TextRecipient : Recipient
    {
        public string Message { get; set; }
        
        public override string ToString()
        {
            return string.Format("[TextRecipient: {0}, Message={1}]", base.ToString(), Message);
        }
    }
}

