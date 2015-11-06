using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Keywords.Model.Request
{
    public class KeywordPurchaseRequest : CallfireModel
    {
        public IList<string> Keywords { get; set; }

        public KeywordPurchaseRequest()
        {
            Keywords = new List<string>();
        }
    }
}

