using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.Contacts.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public abstract class Action<T> : CallfireModel where T: ActionRecord
    {
        public long? Id { get; set; }

        public string FromNumber { get; set; }

        public string ToNumber { get; set; }

        public long? CampaignId { get; set; }

        public long? BatchId { get; set; }

        public Contact Contact { get; set; }

        public bool? Inbound { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public StateType? State { get; set; }

        public IList<string> Labels { get; set; }

        public IDictionary<string, string> Attributes { get; set; }

        public IList<T> Records { get; set; }

        public override string ToString()
        {
            return string.Format("[Action: Id={0}, FromNumber={1}, ToNumber={2}, CampaignId={3}, BatchId={4}, Contact={5},"
                + "Inbound={6}, Created={7}, Modified={8}, State={9}, Labels={10}, Attributes={11}, Records={12}]",
                Id, FromNumber, ToNumber, CampaignId, BatchId, Contact, Inbound, Created, Modified, State,
                Labels?.ToPrettyString(), Attributes?.ToPrettyString(), Records?.ToPrettyString());
        }
    }
}

