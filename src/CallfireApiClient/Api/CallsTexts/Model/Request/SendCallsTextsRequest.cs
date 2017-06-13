using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public abstract class SendCallsTextsRequest : CallfireModel
    {
        public long? CampaignId { get; set; }

        public string Fields { get; set; }

        public bool? StrictValidation { get; set; }

        public override string ToString()
        {
            return string.Format("[SendCallsTextsRequest: campaignId={0}, fields={1}, strictValidation={2}]]",
                CampaignId, Fields, StrictValidation);
        }
    }
}

