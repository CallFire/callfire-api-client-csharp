using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// NumberOrder
    /// </summary>
    public class NumberOrder : CallfireModel
    {
        public long? Id { get; set; }

        public Status? Status { get; private set; }

        public DateTime? Created { get; private set; }

        public double? TotalCost { get; private set; }

        public NumberOrderItem LocalNumbers { get; set; }

        public NumberOrderItem TollFreeNumbers { get; set; }

        public NumberOrderItem Keywords { get; set; }

        public override string ToString()
        {
            return string.Format("[NumberOrder: Id={0}, Status={1}, Created={2}, TotalCost={3}, LocalNumbers={4}, TollFreeNumbers={5}, Keywords={6}]",
                Id, Status, Created, TotalCost, LocalNumbers, TollFreeNumbers, Keywords);
        }
  
    }
}
