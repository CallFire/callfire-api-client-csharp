using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class LocalDate : CallfireModel
    {
        public int? Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }

        public override string ToString()
        {
            return string.Format("[LocalDate: Year={0}, Month={1}, Day={2}]", Year, Month, Day);
        }
    }
}

