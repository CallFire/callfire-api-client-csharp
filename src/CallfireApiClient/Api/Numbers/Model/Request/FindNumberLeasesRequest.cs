using System;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class FindNumberLeasesRequest : FindByRegionDataRequest
    {
        public string LabelName { get; set; }

        public override string ToString()
        {
            return string.Format("[FindNumberLeasesRequest: {0} LabelName={1}]", base.ToString(), LabelName);
        }
    }
}

