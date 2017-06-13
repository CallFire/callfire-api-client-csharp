using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Campaigns.Model.Request
{
    public class AddBatchRequest : CallfireModel
    {
        [JsonIgnore]
        public long CampaignId { get; set; }

        [JsonIgnore]
        public bool? StrictValidation { get; set; }

        public string Name { get; set; }

        public long? ContactListId { get; set; }

        public bool? ScrubDuplicates { get; set; }

        public IList<Recipient> Recipients { get; set; }

        public override string ToString()
        {
            return string.Format("[AddBatchRequest: CampaignId={0}, Name={1}, ContactListId={2}, ScrubDuplicates={3}, Recipients={4}, StrictValidation={5}]",
                CampaignId, Name, ContactListId, ScrubDuplicates, Recipients, StrictValidation);
        }
    }
}

