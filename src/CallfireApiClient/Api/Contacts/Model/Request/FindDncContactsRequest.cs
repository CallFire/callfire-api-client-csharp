using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindDncContactsRequest : FindRequest
    {
        public string Prefix { get; set; }
        public long? DncListId { get; set; }
        public string DncListName { get; set; }
        public bool? CallDnc { get; set; }
        public bool? TextDnc { get; set; }

        public override string ToString()
        {
            return string.Format("[FindDncContactsRequest: {0}, Prefix={1}, DncListId={2}, DncListName={3}, CallDnc={4}, TextDnc={5}]", base.ToString(),
                Prefix, DncListId, DncListName, CallDnc, TextDnc);
        }
    }
}

