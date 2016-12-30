using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindUniversalDncsRequest : CallfireModel
    {
        [JsonIgnore]
        public string ToNumber { get; set; }

        public string FromNumber { get; set; }

        public string Fields { get; set; }

        public override string ToString()
        {
            return string.Format("[FindUniversalDncsRequest: ToNumber={0}, FromNumber={1}, Fields={1}]", ToNumber, FromNumber, Fields);
        }
    }
}