using System;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class NumberPurchaseRequest
    {
        public int? TollFreeCount { get; set; }

        public int? LocalCount { get; set; }

        public string Prefix { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string Lata { get; set; }

        public string RateCenter { get; set; }

        public IList<string> Numbers { get; set; }

        public NumberPurchaseRequest()
        {
            Numbers = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[NumberPurchaseRequest: TollFreeCount={0}, LocalCount={1}, Prefix={2}, City={3}, State={4}, Zipcode={5}, Lata={6}, RateCenter={7}, Numbers={8}]",
                TollFreeCount, LocalCount, Prefix, City, State, Zipcode, Lata, RateCenter, Numbers);
        }
    }
}

