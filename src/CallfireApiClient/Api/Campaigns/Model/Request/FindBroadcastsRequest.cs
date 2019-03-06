using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Campaigns.Model.Request
{
    public class FindBroadcastsRequest : FindRequest
    {
        public  string Label { get; set; }

        public string Name { get; set; }

        public bool? Running { get; set; }

        public bool? Scheduled { get; set; }

        public DateTime? IntervalBegin { get; set; }

        public DateTime? IntervalEnd { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} FindBroadcastsRequest: Label={1}, Name={2}, Running={3}, Scheduled={4}, intervalBegin={5}, intervalEnd={6}]", 
                base.ToString(), Label, Name, Running, IntervalBegin, IntervalEnd);
        }
    }
}

