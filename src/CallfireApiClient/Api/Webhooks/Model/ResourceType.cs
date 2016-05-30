using System.Runtime.Serialization;

namespace CallfireApiClient.Api.Webhooks.Model
{
    public enum ResourceEvent
    {
        [EnumMember(Value = "Started")]
        STARTED,
        [EnumMember(Value = "Stopped")]
        STOPPED,
        [EnumMember(Value = "Finished")]
        FINISHED,
        [EnumMember(Value = "Failed")]
        FAILED,
        [EnumMember(Value = "unknown")]
        UNKNOWN
    }

    public enum ResourceType
    {
        [EnumMember(Value = "MonthlyRenewal")]
        MONTHLY_RENEWAL,
        [EnumMember(Value = "LowBalance")]
        LOW_BALANCE,
        [EnumMember(Value = "CccCampaign")]
        CCC_CAMPAIGN,
        [EnumMember(Value = "CallBroadcast")]
        CALL_BROADCAST,
        [EnumMember(Value = "TextBroadcast")]
        TEXT_BROADCAST,
        [EnumMember(Value = "OutboundCall")]
        OUTBOUND_CALL,
        [EnumMember(Value = "InboundCall")]
        INBOUND_CALL,
        [EnumMember(Value = "OutboundText")]
        OUTBOUND_TEXT,
        [EnumMember(Value = "InboundText")]
        INBOUND_TEXT,
        [EnumMember(Value = "unknown")]
        UNKNOWN
    }
}