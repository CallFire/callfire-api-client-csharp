using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// NumberOrderItem
    /// </summary>
    public class NumberOrderItem : CallfireModel
    {
        public int? Ordered { get; set; }

        public double? UnitCost { get; private set; }

        public IList<string> Fulfilled { get; private set; }

        public NumberOrderItem()
        {
            Ordered = 0;
        }

        public override string ToString()
        {
            return string.Format("[NumberOrderItem: Ordered={0}, UnitCost={1}, Fulfilled={2}]", Ordered, UnitCost,
                Fulfilled?.ToPrettyString());
        }
    }
}
