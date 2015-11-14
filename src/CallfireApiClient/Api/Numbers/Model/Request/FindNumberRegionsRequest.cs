using System;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class FindNumberRegionsRequest : FindByRegionDataRequest
    {
        public override string ToString()
        {
            return string.Format("[FindNumberRegionsRequest {0}]", base.ToString());
        }
    }
}

