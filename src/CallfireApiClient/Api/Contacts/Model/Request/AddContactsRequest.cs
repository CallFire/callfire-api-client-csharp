using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddContactsRequest<T> : CallfireModel
    {
       
        [JsonIgnore]
        public List<T> Contacts { get; set; }

        protected AddContactsRequest()
        {
        }

        public override string ToString()
        {
            return string.Format("[AddContactsRequest: Contacts={0}]", Contacts?.ToPrettyString());
        }

    }
}

