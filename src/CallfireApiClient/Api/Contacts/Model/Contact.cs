using System;
using System.Collections.Generic;
using System.Linq;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class Contact
    {
        public long? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Zipcode { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public string MobilePhone { get; set; }

        /// <summary>
        /// External id of contact for syncing with external sources
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// External system that external id refers to
        /// </summary>
        public string ExternalSystem { get; set; }

        /// <summary>
        /// Is contact deleted
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Map of string properties for contact
        /// </summary>
        public IDictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return string.Format("[Contact: Id={0}, FirstName={1}, LastName={2}, Zipcode={3}, HomePhone={4}, WorkPhone={5}, MobilePhone={6}, ExternalId={7}, ExternalSystem={8}, Deleted={9}, Properties={10}]",
                Id, FirstName, LastName, Zipcode, HomePhone, WorkPhone, MobilePhone, ExternalId, ExternalSystem, Deleted,
                Properties?.ToPrettyString());
        }
    }
}

