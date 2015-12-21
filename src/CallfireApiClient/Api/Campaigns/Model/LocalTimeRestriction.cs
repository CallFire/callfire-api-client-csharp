using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class LocalTimeRestriction : CallfireModel
    {
        public bool? Enabled { get; set; }

        public int? BeginHour { get; set; }

        public int? BeginMinute { get; set; }

        public int? EndHour { get; set; }

        public int? EndMinute { get; set; }

        public override string ToString()
        {
            return string.Format("[LocalTimeRestriction: enabled={0}, beginHour={1}, beginMinute={2}, endHour={3}, endMinute={4}]",
                Enabled, BeginHour, BeginMinute, EndHour, EndMinute);
        }
    }
}

