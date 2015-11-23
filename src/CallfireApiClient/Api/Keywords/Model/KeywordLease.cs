using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Keywords.Model
{
    public class KeywordLease : Keyword
    {
        public DateTime? leaseBegin { get; set; }
        public DateTime? leaseEnd { get; set; }
        public bool? autoRenew { get; set; }
        public LeaseStatus? status { get; set; }

        public override string ToString()
        {
            return string.Format("[KeywordLease: leaseBegin={0}, leaseEnd={1},  autoRenew={2},  status={3}",
                leaseBegin, leaseEnd, autoRenew, status);
        }
    }
}