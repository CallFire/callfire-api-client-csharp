using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CallCreateSound : CallfireModel
    {
        public string Name { get; set; }
        public string ToNumber { get; set; }

        public override string ToString()
        {
            return string.Format("[CallCreateSound: Name={0}, ToNumber={1}]",
                Name, ToNumber);
        }
    }
}

