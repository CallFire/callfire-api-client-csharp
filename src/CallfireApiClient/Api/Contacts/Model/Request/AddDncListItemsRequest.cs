using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddDncListItemsRequest<T> : AddContactsRequest<T>
    {
        [JsonIgnore]
        public long? contactListId { get; set; }

        public AddDncListItemsRequest()
        {
        }

    }
}

