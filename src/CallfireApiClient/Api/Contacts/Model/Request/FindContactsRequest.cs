using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;
using System.Linq;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindContactsRequest : FindRequest
    {
        /// <summary>
        /// Contact list id to search by
        /// </summary>
        public long? ContactListId { get; set; }

        /// <summary>
        /// Contact property to search by
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Value of contact property to search by
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// Multiple contact numbers can be specified. If the number parameter is included,
        /// the other query parameters are ignored.
        /// </summary>
        public IList<string> Number { get; set; }

        /// <summary>
        /// Multiple contact ids can be specified. If the id parameter is included,
        /// the other query parameters are ignored.
        /// </summary>
        public IList<long> Id { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [FindContactsRequest: ContactListId={1}, PropertyName={2}, PropertyValue={3}, Number={4}, Id={5}]",
                base.ToString(), ContactListId, PropertyName, PropertyValue, Number?.ToPrettyString(), Id?.ToPrettyString());
        }
    }
}

