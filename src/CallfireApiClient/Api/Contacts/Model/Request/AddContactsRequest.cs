﻿using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddContactsRequest<T> : CallfireModel
    {
        private const string FIELD_CONTACT_IDS = "contactIds";
        private const string FIELD_CONTACT_NUMBERS = "contactNumbers";
        private const string FIELD_CONTACTS = "contacts";

        [JsonExtensionData]
        private IDictionary<string, object> ContactsData;

        [JsonIgnore]
        public List<T> Contacts { get; set; }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            if (Contacts != null && Contacts.Count > 0)
            {
                ContactsData = new Dictionary<string, object>(1);
                object item = Contacts[0];
                if (item is long)
                {
                    ContactsData.Add(FIELD_CONTACT_IDS, Contacts);
                }
                else if (item is string)
                {
                    ContactsData.Add(FIELD_CONTACT_NUMBERS, Contacts);
                }
                else if (item is Contact || item is DoNotContact)
                {
                    ContactsData.Add(FIELD_CONTACTS, Contacts);
                }
                else
                {
                    throw new System.InvalidOperationException("Type " + item.GetType().ToString() +
                        " isn't supported to create contacts. Use long, string or Contact/DoNotComtact types instead.");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[AddContactsRequest: Contacts={0}]", Contacts?.ToPrettyString());
        }

    }
}

