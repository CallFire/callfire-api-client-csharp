using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class UpdateContactListRequest : CallfireModel
    {
        [JsonIgnore]
        public long? Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("[UpdateContactListRequest: Id={0}, Name={1}]", Id, Name);
        }
    }
}

