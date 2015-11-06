using System;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    public class NumberPurchaseRequest
    {
        public int tollFreeCount { get; set; }

        public int localCount { get; set; }

        public string prefix { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zipcode { get; set; }

        public string lata { get; set; }

        public string rateCenter { get; set; }

        public IList<string> Numbers { get; set; }

        public NumberPurchaseRequest()
        {
            Numbers = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[NumberPurchaseRequest: tollFreeCount={0}, localCount={1}, prefix={2}, city={3}, state={4}, zipcode={5}, lata={6}, rateCenter={7}, Numbers={8}]",
                tollFreeCount, localCount, prefix, city, state, zipcode, lata, rateCenter, Numbers);
        }
    }
}

