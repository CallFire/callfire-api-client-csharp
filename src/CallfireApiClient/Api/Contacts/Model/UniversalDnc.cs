using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class UniversalDnc : CallfireModel
    {
        public string ToNumber { get; set; }
        public string FromNumber { get; set; }
        public bool? InboundCall { get; set; }
        public bool? InboundText { get; set; }
        public bool? OutboundCall { get; set; }
        public bool? OutboundText { get; set; }

        public override string ToString()
        {
            return string.Format("[UniversalDnc: toNumber={0}, fromNumber={1}, inboundCall={2}, inboundText={3}, outboundCall={4}, outboundText={5}",
                ToNumber, FromNumber, InboundCall, InboundText, OutboundCall, OutboundText);
        }
    }
}