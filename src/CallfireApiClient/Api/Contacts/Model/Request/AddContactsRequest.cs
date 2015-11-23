using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddContactsRequest<T> : CallfireModel
    {
       
        [JsonIgnore]
        public List<T> contacts { get; set; }

        protected AddContactsRequest()
        {
        }

    }
}

