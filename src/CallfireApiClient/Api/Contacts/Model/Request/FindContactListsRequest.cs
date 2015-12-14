using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindContactListsRequest : FindRequest
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [FindContactListsRequest: Name={1}]",
                base.ToString(), Name);
        }
    }
}

