using System;
using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class CampaignSound : CallfireModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public DateTime? Created { get; set; }

        public int? LengthInSeconds { get; set; }

        [JsonProperty("status")]
        public Status? StatusString { get; set; }

        public enum Status
        {
            UPLOAD, RECORDING, ACTIVE, FAILED, ARCHIVED
        }

        public override string ToString()
        {
            return string.Format("[CampaignSound: Id={0}, Name={1}, StatusString={2}, Created={3}, lengthInSeconds={4}]",
                Id, Name, StatusString, Created, Created, LengthInSeconds);
        }
    }
}

