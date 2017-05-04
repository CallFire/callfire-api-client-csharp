using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class AddContactListContactsRequest<T> : AddContactsRequest<T>
    {
        [JsonIgnore]
        public long? ContactListId { get; set; }

        public override string ToString()
        {
            return string.Format("[AddContactListItemsRequest: {0}, ContactListId={1}, ContactNumbersField={2}]",
                base.ToString(), ContactListId, ContactNumbersField);
        }
    }
}

