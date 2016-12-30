using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindDncNumbersRequest : FindRequest
    {
        public string Prefix { get; set; }
        public long? CampaignId { get; set; }
        public string Source { get; set; }
        public bool? Call { get; set; }
        public bool? Text { get; set; }
        public List<string> Numbers { get; set; }

        public override string ToString()
        {
            return string.Format("[FindDncContactsRequest: {0}, Prefix={1}, CampaignId={2}, Source={3}, Call={4}, Text={5}, Numbers={6}]", base.ToString(),
                Prefix, CampaignId, Source, Call, Text, Numbers.ToPrettyString());
        }
    }
}

