using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// Object represents Plan usage statistics
    /// </summary>
    public class BillingPlanUsage : CallfireModel
    {
        /// <summary>
        /// Gets start of usage period
        /// </summary>
        /// <value>start of usage period</value>
        public DateTime IntervalStart { get; private set; }

        /// <summary>
        /// Gets end of usage period
        /// </summary>
        /// <value>end of usage period</value>
        public DateTime IntervalEnd { get; private set; }

        /// <summary>
        /// Gets remaining plan credits rounded to nearest whole value
        /// </summary>
        /// <value>remaining plan credits rounded to nearest whole value</value>
        public Decimal RemainingPlanCredits { get; private set; }

        /// <summary>
        /// Gets remaining pay as you go credits rounded to nearest whole value.
        /// </summary>
        /// <value>remaining pay as you go credits rounded to nearest whole value.</value>
        public Decimal RemainingPayAsYouGoCredits { get; private set; }

        /// <summary>
        /// Gets total remaining credits (remainingPlanCredits + remainingPayAsYouGoCredits)
        /// </summary>
        /// <value>total remaining credits (remainingPlanCredits + remainingPayAsYouGoCredits)</value>
        public Decimal TotalRemainingCredits { get; private set; }

        public override string ToString()
        {
            return string.Format("[BillingPlanUsage: IntervalStart={0}, IntervalEnd={1}, RemainingPlanCredits={2}, RemainingPayAsYouGoCredits={3}, TotalRemainingCredits={4}]",
                IntervalStart, IntervalEnd, RemainingPlanCredits, RemainingPayAsYouGoCredits, TotalRemainingCredits);
        }
    }
}