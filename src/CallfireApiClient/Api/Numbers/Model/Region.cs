using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class Region : CallfireModel
    {
        public string Prefix { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string Country { get; set; }

        public string Lata { get; set; }

        public string RateCenter { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string TimeZone { get; set; }

        public override string ToString()
        {
            return string.Format("[Region: Prefix={0}, City={1}, State={2}, Zipcode={3}, Country={4}, Lata={5}, RateCenter={6}, Latitude={7}, Longitude={8}, TimeZone={9}]",
                Prefix, City, State, Zipcode, Country, Lata, RateCenter, Latitude, Longitude, TimeZone);
        }
    }
}

