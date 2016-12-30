using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class ActionRecord : CallfireModel
    {
        public long? Id { get; set; }

        public double? BilledAmount { get; private set; }

        public DateTime? FinishTime { get; private set; }

        public string ToNumber { get; private set; }

        public string CallerName { get; private set; }

        public string SwitchId { get; private set; }

        public ISet<string> Labels { get; private set; }

        public override string ToString()
        {
            return string.Format("[ActionRecord: Id={0}, billedAmount={1}, FinishTime={2}, ToNumber={3}, CallerName={4}, SwitchId={5}, Labels={6}]", 
                Id, BilledAmount, FinishTime, ToNumber, CallerName, SwitchId, Labels?.ToPrettyString());
        }
    }
}

