using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class ActionRecord : CallfireModel
    {
        public long? Id { get; set; }

        public double? BilledAmount { get; private set; }

        public DateTime? FinishTime { get; private set; }

        public override string ToString()
        {
            return string.Format("[ActionRecord: Id={0}, billedAmount={1}, FinishTime={2}]", Id, BilledAmount, FinishTime);
        }
    }
}

