using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddDncListItemsRequest<T> : AddContactsRequest<T>
    {
        [JsonIgnore]
        public long? ContactListId { get; set; }

        public override string ToString()
        {
            return string.Format("[AddDncListItemsRequest: {0}, ContactListId={1}]", base.ToString(), ContactListId);
        }
    }
}

