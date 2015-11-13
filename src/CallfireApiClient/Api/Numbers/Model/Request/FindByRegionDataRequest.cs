using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public abstract class FindByRegionDataRequest : FindRequest
    {
        /// <summary>
        /// 4-7 digit prefix
        /// </summary>
        public string Prefix { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 2 letter state code
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 5 digit zipcode
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// Local access and transport area (LATA) code
        /// </summary>
        public string Lata { get; set; }

        /// <summary>
        /// rate center code
        /// </summary>
        public string RateCenter { get; set; }

        public override string ToString()
        {
            return string.Format("[FindByRegionDataRequest: {0} Prefix={1}, City={2}, State={3}, Zipcode={4}, Lata={5}, RateCenter={6}]",
                base.ToString(), Prefix, City, State, Zipcode, Lata, RateCenter);
        }
    }
}

