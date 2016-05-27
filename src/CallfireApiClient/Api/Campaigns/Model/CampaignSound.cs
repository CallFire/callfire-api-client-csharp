using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CampaignSound : CallfireModel
    {
        public long? Id { get; private set; }

        public string Name { get; private set; }

        public DateTime? Created { get; private set; }

        public int? LengthInSeconds { get; private set; }

        public SoundStatus? Status { get; private set; }

        public bool? Duplicate { get; private set; }

        public enum SoundStatus
        {
            UPLOAD,
            RECORDING,
            ACTIVE,
            FAILED,
            ARCHIVED
        }

        public override string ToString()
        {
            return string.Format("[CampaignSound: Id={0}, Name={1}, StatusString={2}, Created={3}, lengthInSeconds={4}, status={4}, duplicate={4}]",
                Id, Name, Status, Created, LengthInSeconds, Status, Duplicate);
        }
    }
}

