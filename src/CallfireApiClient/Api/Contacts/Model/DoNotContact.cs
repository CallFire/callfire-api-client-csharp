using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class DoNotContact : CallfireModel
    {
        public string Number { get; private set; }
        public bool? Call { get; private set; }
        public bool? Text { get; private set; }
        public long? CampaignId { get; private set; }
        public string Source { get; private set; }
        public DateTime? Created { get; private set; }
     
        public override string ToString()
        {
            return string.Format("[DoNotContact: number={0}, call={1}, text={2}, campaignId={3}, source={4}, created={5}",
                Number, Call, Text, CampaignId, Source, Created);
        }
    }
}