using System;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Common.Model
{
    public class WeeklySchedule : CallfireModel
    {
        public LocalTime StartTimeOfDay { get; set; }

        public LocalTime StopTimeOfDay { get; set; }

        public ISet<DayOfWeek> DaysOfWeek { get; set; }

        public string TimeZone { get; set; }

        public override string ToString()
        {
            return string.Format("[WeeklySchedule: StartTimeOfDay={0}, StopTimeOfDay={1}, DaysOfWeek={2}, TimeZone={3}]",
                StartTimeOfDay, StopTimeOfDay, DaysOfWeek?.ToPrettyString(), TimeZone);
        }
    }
}

