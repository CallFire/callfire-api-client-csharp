using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class LocalTimeRestriction : CallfireModel
    {
        public bool? enabled { get; set; }

        public int? beginHour{ get; set; }

        public int? beginMinute{ get; set; }

        public int? endHour{ get; set; }

        public int? endMinute{ get; set; }

        public override string ToString()
        {
            return string.Format("[LocalTimeRestriction: enabled={0}, beginHour={1}, beginMinute={2}, endHour={3}, endMinute={4}]",
                enabled, beginHour, beginMinute, endHour, endMinute);
        }
    }
}

