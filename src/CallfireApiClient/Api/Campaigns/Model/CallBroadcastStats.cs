using System;

namespace CallfireApiClient.Api.Campaigns.Model
{
    /// <summary>
    /// Statistics information about CallBroadcast
    /// </summary>
    public class CallBroadcastStats : BroadcastStats
    {
        // Usage Stats
        public int? CallsAttempted { get; private set; }

        public int? CallsPlaced { get; private set; }

        public int? CallsDuration { get; private set; }

        public int? BilledDuration { get; private set; }

        public int? ResponseRatePercent { get; private set; }
        // (100 * CallsLiveAnswer / CallsPlaced)

        // Action Stats
        public int? CallsRemaining { get; private set; }

        public int? CallsAwaitingRedial { get; private set; }

        public int? CallsLiveAnswer { get; private set; }

        // Result Stats
        public int? TotalCount { get; private set; }

        public int? AnsweringMachineCount { get; private set; }

        public int? BusyCount { get; private set; }

        public int? DialedCount { get; private set; }

        public int? DoNotCallCount { get; private set; }

        public int? ErrorCount { get; private set; }

        public int? LiveCount { get; private set; }

        public int? MiscCount { get; private set; }

        public int? NoAnswerCount { get; private set; }

        public int? TransferCount { get; private set; }

        public override string ToString()
        {
            return string.Format("[{18} CallBroadcastStats: CallsAttempted={0}, CallsPlaced={1}, CallsDuration={2}, BilledDuration={3}, ResponseRatePercent={4}, CallsRemaining={5}, CallsAwaitingRedial={6}, CallsLiveAnswer={7}, TotalCount={8}, AnsweringMachineCount={9}, BusyCount={10}, DialedCount={11}, DoNotCallCount={12}, ErrorCount={13}, LiveCount={14}, MiscCount={15}, NoAnswerCount={16}, TransferCount={17}]",
                CallsAttempted, CallsPlaced, CallsDuration, BilledDuration, ResponseRatePercent, CallsRemaining,
                CallsAwaitingRedial, CallsLiveAnswer, TotalCount, AnsweringMachineCount, BusyCount, DialedCount,
                DoNotCallCount, ErrorCount, LiveCount, MiscCount, NoAnswerCount, TransferCount, base.ToString());
        }
        
    }
}

