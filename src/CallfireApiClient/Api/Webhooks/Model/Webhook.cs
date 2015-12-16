using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public class Webhook : CallfireModel
    {
        public long? Id { get; set; }

        public bool? Enabled { get; set; }

        public string Name { get; set; }

        public string Resource { get; set; }

        public bool? NonStrictSsl { get; set; }

        public string Fields { get; set; }

        public string Callback { get; set; }

        public string Secret { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ISet<string> Events { get; set; }

        public override string ToString()
        {
            return string.Format("[Webhook: Id={0}, Enabled={1}, Name={2}, Resource={3}, NonStrictSsl={4}, Fields={5}, Callback={6}, Secret={7}, CreatedAt={8}, UpdatedAt={9}, Events={10}]",
                Id, Enabled, Name, Resource, NonStrictSsl, Fields, Callback, Secret, CreatedAt, UpdatedAt, Events?.ToPrettyString());
        }

        public static class ResourceType
        {
            public const string VOICE_BROADCAST = "voiceCampaign";
            public const string TEXT_BROADCAST = "textCampaign";
            public const string IVR_BROADCAST = "ivrCampaign";
            public const string CCC_CAMPAIGN = "cccCampaign";
        }

        public static class ResourceEvent
        {
            public const string STARTED = "start";
            public const string STOPPED = "stop";
            public const string FINISHED = "finish";
        }
    }
}

