using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindMediaRequest : FindRequest
    {
        public string Filter { get; set; }
        
        public override string ToString()
        {
            return string.Format("[FindMediaRequest: {0}, filter={1}]", base.ToString(), Filter);
        }
    }
}