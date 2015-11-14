using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class IvrInboundConfig : CallfireModel
    {
        public string DialplanXml { get; set; }

        public override string ToString()
        {
            return string.Format("[IvrInboundConfig: DialplanXml={0}]", DialplanXml);
        }
    }
}

