using System;
using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class ContactList : CallfireModel
    {
        public long? Id;
        public string Name;
        public int? Size;
        public DateTime? Created;

        [JsonProperty("status")]
        public Status StatusName;

        public enum Status
        {
            ACTIVE,
            VALIDATING,
            IMPORTING,
            IMPORT_FAILED,
            ERRORS,
            DELETED,
            PARSE_FAILED,
            COLUMN_TOO_LARGE
        }

        public override string ToString()
        {
            return string.Format("[ContactList: Id={0}, Name={1}, Size={2}, Created={3}, Status={4}",
                Id, Name, Size, Name, Created);
        }
    }
}