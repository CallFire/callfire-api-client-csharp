using System;
using CallfireApiClient.Api.Keywords.Model;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class NumberLease : Number
    {
        public DateTime? LeaseBegin { get; set; }

        public DateTime? LeaseEnd { get; set; }

        public bool? AutoRenew { get; set; }

        public LeaseStatus? Status { get; set; }

        public FeatureStatus? CallFeatureStatus { get; set; }

        public FeatureStatus? TextFeatureStatus { get; set; }

        public enum FeatureStatus
        {
            UNSUPPORTED,
            PENDING,
            DISABLED,
            ENABLED
        }

        public override string ToString()
        {
            return string.Format("[NumberLease: {0} LeaseBegin={1}, LeaseEnd={2}, AutoRenew={3}, Status={4}, CallFeatureStatus={5}, TextFeatureStatus={6}]",
                base.ToString(), LeaseBegin, LeaseEnd, AutoRenew, Status, CallFeatureStatus, TextFeatureStatus);
        }
    }
}

