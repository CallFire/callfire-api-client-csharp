using System;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class FindNumbersLocalRequest : FindByRegionDataRequest
    {
        public override string ToString()
        {
            return string.Format("[FindNumbersLocalRequest {0}]", base.ToString());
        }
    }
}

