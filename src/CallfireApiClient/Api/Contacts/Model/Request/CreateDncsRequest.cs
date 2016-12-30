using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class CreateDncsRequest : CallsTextsRequest
    {
        public string Source { get; set; }

        public List<string> Numbers { get; set; }

        public CreateDncsRequest()
        {
            Numbers = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[CreateDncsRequest: {0}, Source={1}, Numbers={2}]", base.ToString(),
                Source, Numbers.ToPrettyString());
        }
    }
}

