using System;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public enum BroadcastStatus
    {
        TEST,
        SETUP,
        START_PENDING,
        RUNNING,
        STOPPED,
        FINISHED,
        ARCHIVED,
        SCHEDULED,
        SUSPENDED,
        VALIDATING_EMAIL,
        VALIDATING_START
    }
}