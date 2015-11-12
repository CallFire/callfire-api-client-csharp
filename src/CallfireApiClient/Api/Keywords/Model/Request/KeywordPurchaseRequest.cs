using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace CallfireApiClient.Api.Keywords.Model.Request
{
    public class KeywordPurchaseRequest : CallfireModel
    {
        public IList<string> Keywords { get; set; }

        public KeywordPurchaseRequest()
        {
            Keywords = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[KeywordPurchaseRequest: Keywords={0}]", Keywords?.ToPrettyString());
        }
    }
}

