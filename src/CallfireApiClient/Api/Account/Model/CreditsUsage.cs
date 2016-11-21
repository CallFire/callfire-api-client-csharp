using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// Object represents Credits usage statistics
    /// </summary>
    public class CreditsUsage : CallfireModel
    {
        /// <summary>
        /// Gets beginning of usage period
        /// </summary>
        /// <value>beginning of usage period</value>
        public DateTime IntervalBegin { get; private set; }

        /// <summary>
        /// Gets end of usage period
        /// </summary>
        /// <value>end of usage period</value>
        public DateTime IntervalEnd { get; private set; }

        /// <summary>
        /// Gets sum of calls duration rounded to nearest whole minute
        /// </summary>
        /// <value>sum of calls duration rounded to nearest whole minute</value>
        public int CallsDurationMinutes { get; private set; }

        /// <summary>
        /// Gets number of texts sent
        /// </summary>
        /// <value>number of texts sent</value>
        public int TextsSent { get; private set; }

        /// <summary>
        /// Gets total credits used by textsSent and callsDurationMinutes
        /// </summary>
        /// <value>total credits used by textsSent and callsDurationMinutes</value>
        public Decimal CreditsUsed { get; private set; }

        public override string ToString()
        {
            return string.Format("[CreditsUsage: IntervalBegin={0}, IntervalEnd={1}, CallsDurationMinutes={2}, TextsSent={3}, CreditsUsed={4}]",
                IntervalBegin, IntervalEnd, CallsDurationMinutes, TextsSent, CreditsUsed);
        }
    }
}