using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class NumberConfig : CallfireModel
    {
        public string Number { get; set; }

        public NumberConfigType? ConfigType { get; set; }

        public CallTrackingConfig CallTrackingConfig { get; set; }

        public IvrInboundConfig IvrInboundConfig { get; set; }

        public enum NumberConfigType
        {
            IVR,
            TRACKING
        }

        public override string ToString()
        {
            return string.Format("[NumberConfig: Number={0}, ConfigType={1}, CallTrackingConfig={2}, IvrInboundConfig={3}]",
                Number, ConfigType, CallTrackingConfig, IvrInboundConfig);
        }
    }
}

