using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindDncListsRequest : FindRequest
    {
        public long? campaignId { get; set; }
        public string name { get; set; }
    }
}

