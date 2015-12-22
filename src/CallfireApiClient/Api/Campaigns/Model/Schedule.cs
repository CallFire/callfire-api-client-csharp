using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class Schedule : CallfireModel
    {
        public long? Id { get; set; }

        public long? CampaignId { get; set; }

        public LocalDate StartDate { get; set; }

        public LocalTime StartTimeOfDay { get; set; }

        public LocalDate StopDate { get; set; }

        public LocalTime StopTimeOfDay { get; set; }

        public string TimeZone { get; set; }

        public ISet<DayOfWeek> DaysOfWeek { get; set; }

        public override string ToString()
        {
            return string.Format("[Schedule: Id={0}, CampaignId={1}, StartDate={2}, StartTimeOfDay={3}, StopDate={4}, StopTimeOfDay={5}, TimeZone={6}, DaysOfWeek={7}]",
                Id, CampaignId, StartDate, StartTimeOfDay, StopDate, StopTimeOfDay, TimeZone, DaysOfWeek);
        }
    }
}

