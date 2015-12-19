using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class RetryConfig : CallfireModel
    {
        public int? MaxAttempts;

        public int? MinutesBetweenAttempts { get; set; }

        public IList<RetryResults> RetryResults { get; set; }

        public IList<RetryPhoneTypes> RetryPhoneTypes { get; set; }

        public override string ToString()
        {
            return string.Format("[RetryConfig: MaxAttempts={0}, MinutesBetweenAttempts={1}, RetryResults={2}, RetryPhoneTypes={3}]", 
                MaxAttempts, MinutesBetweenAttempts, RetryResults, RetryPhoneTypes);
        }
    }
}

