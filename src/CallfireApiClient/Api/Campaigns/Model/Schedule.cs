using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class Schedule : WeeklySchedule
    {
        public long? Id { get; set; }

        public long? CampaignId { get; set; }

        public LocalDate StartDate { get; set; }

        public LocalDate StopDate { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [Schedule: Id={1}, CampaignId={2}, StartDate={3}, StopDate={4}]", base.ToString(),
                Id, CampaignId, StartDate, StopDate);
        }
    }
}

