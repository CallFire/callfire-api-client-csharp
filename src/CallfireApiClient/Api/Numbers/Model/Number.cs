using System;
using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class Number : CallfireModel
    {
        [JsonProperty("number")]
        public string PhoneNumber { get; set; }

        public string NationalFormat { get; set; }

        public bool? TollFree { get; set; }

        public Region Region { get; set; }

        public override string ToString()
        {
            return string.Format("[Number: PhoneNumber={0}, NationalFormat={1}, TollFree={2}, Region={3}]",
                PhoneNumber, NationalFormat, TollFree, Region);
        }
    }
}

