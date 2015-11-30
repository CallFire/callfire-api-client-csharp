using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class Batch : CallfireModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public BatchStatus? Status { get; set; }

        public long? BroadcastId { get; set; }

        public DateTime? Created { get; private set; }

        public int? Size { get; private set; }

        public int? Remaining { get; private set; }

        public bool? Enabled { get; set; }

        public enum BatchStatus
        {
            NEW,
            VALIDATING,
            ERRORS,
            SOURCE_ERROR,
            ACTIVE,
        }

        public override string ToString()
        {
            return string.Format("[Batch: Id={0}, Name={1}, Status={2}, BroadcastId={3}, Created={4}, Size={5}, Remaining={6}, Enabled={7}]",
                Id, Name, Status, BroadcastId, Created, Size, Remaining, Enabled);
        }
    }
}

