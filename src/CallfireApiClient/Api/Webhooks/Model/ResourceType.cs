using System.Runtime.Serialization;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public enum ResourceEvent
    {
        [EnumMember(Value = "start")]
        STARTED,
        [EnumMember(Value = "stop")]
        STOPPED,
        [EnumMember(Value = "finish")]
        FINISHED,
        [EnumMember(Value = "unknown")]
        UNKNOWN
    }

    public enum ResourceType
    {
        [EnumMember(Value = "voiceCampaign")]
        VOICE_BROADCAST,
        [EnumMember(Value = "textCampaign")]
        TEXT_BROADCAST,
        [EnumMember(Value = "ivrCampaign")]
        IVR_BROADCAST,
        [EnumMember(Value = "cccCampaign")]
        CCC_CAMPAIGN,
        [EnumMember(Value = "unknown")]
        UNKNOWN
    }
}

