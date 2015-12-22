using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Keywords.Model
{
    public class KeywordLease : Keyword
    {
        public DateTime? LeaseBegin { get; set; }

        public DateTime? LeaseEnd { get; set; }

        public bool? AutoRenew { get; set; }

        public LeaseStatus? Status { get; set; }

        public override string ToString()
        {
            return string.Format("[KeywordLease: leaseBegin={0}, leaseEnd={1},  autoRenew={2},  status={3}",
                LeaseBegin, LeaseEnd, AutoRenew, Status);
        }
    }
}