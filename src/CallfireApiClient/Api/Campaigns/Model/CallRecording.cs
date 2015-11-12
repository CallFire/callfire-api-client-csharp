using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CallRecording : CallfireModel
    {
        public long? Id { get; set; }

        public long? CallId { get; set; }

        public long? CampaignId { get; set; }

        public string Name { get; set; }

        public DateTime? Created { get; set; }

        public long? LengthInBytes { get; set; }

        public int? LengthInSeconds { get; set; }

        public string Hash { get; set; }

        public string Mp3Url { get; set; }

        public CallRecordingState? State { get; set; }

        public enum CallRecordingState
        {
            RECORDING,
            READY,
            ERROR
        }

        public override string ToString()
        {
            return string.Format("[CallRecording: Id={0}, CallId={1}, CampaignId={2}, Name={3}, Created={4}, LengthInBytes={5}, LengthInSeconds={6}, Hash={7}, Mp3Url={8}, State={9}]",
                Id, CallId, CampaignId, Name, Created, LengthInBytes, LengthInSeconds, Hash, Mp3Url, State);
        }
    }
}

