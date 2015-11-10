using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

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
            Fulfilled = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("[NumberOrderItem: Ordered={0}, UnitCost={1}, Fulfilled={2}]", Ordered, UnitCost, Fulfilled);
        }
    }
}
