using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class CreateContactListRequest<T> : AddContactsRequest<T>
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateContactListRequest: {0}, Name={1}]", base.ToString(), Name);
        }
    }
}

