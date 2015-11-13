using System;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class FindNumberLeaseConfigsRequest : FindByRegionDataRequest
    {
        public string LabelName { get; set; }

        public override string ToString()
        {
            return string.Format("[FindNumberLeaseConfigsRequest: {0} LabelName={1}]", base.ToString(), LabelName);
        }
    }
}

