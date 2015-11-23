using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Keywords.Model
{
    public class Keyword : CallfireModel
    {
        public string shortCode { get; set; }
        public string keyword { get; set; }

        public override string ToString()
        {
            return string.Format("[Keyword: shortCode={0}, keyword={1}",
                shortCode, keyword);
        }
    }
}