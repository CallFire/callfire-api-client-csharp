using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class DncList : CallfireModel
    {
        public long? Id { get; set; }
        public int? Size { get; set; }
        public long? CampaignId { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public override string ToString()
        {
            return string.Format("[DncList: id={0}, size={1}, campaignId={2}, name={3}, created={4}",
                Id, Size, CampaignId, Name, Created);
        }
    }
}