using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindDncListsRequest : FindRequest
    {
        public long? CampaignId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("[FindNumberLeaseConfigsRequest: {0} CampaignId={1}, Name={2}]", base.ToString(), CampaignId, Name);
        }
    }
}

