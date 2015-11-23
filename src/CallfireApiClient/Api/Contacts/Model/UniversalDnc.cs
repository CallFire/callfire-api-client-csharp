using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class UniversalDnc : CallfireModel
    {
        public string toNumber { get; set; }
        public string fromNumber { get; set; }
        public bool? inboundCall { get; set; }
        public bool? inboundText { get; set; }
        public bool? outboundCall { get; set; }
        public bool? outboundText { get; set; }

        public override string ToString()
        {
            return string.Format("[UniversalDnc: toNumber={0}, fromNumber={1}, inboundCall={2}, inboundText={3}, outboundCall={4}, outboundText={5}",
                toNumber, fromNumber, inboundCall, inboundText, outboundCall, outboundText);
        }
    }
}