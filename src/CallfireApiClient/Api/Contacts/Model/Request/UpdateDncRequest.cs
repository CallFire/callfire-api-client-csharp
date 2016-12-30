
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class UpdateDncRequest : CallsTextsRequest
    {
        [JsonIgnore]
        public string Number { get; set; }

        public override string ToString()
        {
            return string.Format("[UpdateDncRequest: {0}, Number={1}]", base.ToString(), Number);
        }
    }
}

