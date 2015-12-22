﻿using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class Recipient : CallfireModel
    {
        public string PhoneNumber { get; set; }

        public long? ContactId { get; set; }

        public IDictionary<string, string> Attributes = new Dictionary<string, string>();

        public override string ToString()
        {
            return string.Format("[Recipient: phoneNumber={0}, contactId={1}, attributes={2}]",
                PhoneNumber, ContactId, Attributes.ToPrettyString());
        }
    }
}

