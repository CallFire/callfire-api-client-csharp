using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model.Request
{
    /// <summary>
    /// Request object for GET /me/billing/plan-usage
    /// <summary>
    public class DateIntervalRequest : CallfireModel
    {
        /// <summary>
        /// Sets beginning of usage period
        /// </summary>
        /// <value>beginning of usage period</value>
        public DateTime? IntervalBegin { get; set; }

        /// <summary>
        /// Sets end of usage period
        /// </summary>
        /// <value>end of usage period</value>
        public DateTime? IntervalEnd { get; set; }

        public override string ToString()
        {
            return string.Format("[DateIntervalRequest: IntervalBegin={0}, IntervalEnd={1}]", IntervalBegin, IntervalEnd);
        }
    }
}