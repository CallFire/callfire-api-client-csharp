using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Campaigns.Model
{
    /// <summary>
    /// Represents base broadcast properties for Text and Call broadcasts
    /// </summary>
    public abstract class Broadcast : CallfireModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public BroadcastStatus? Status { get; private set; }

        public DateTime? LastModified { get; private set; }

        public string FromNumber { get; set; }

        public LocalTimeRestriction LocalTimeRestriction { get; set; }

        public IList<Schedule> Schedules { get; set; }

        public int? MaxActive { get; set; }

        public IList<string> Labels { get; set; }

        public override string ToString()
        {
            return string.Format("[Broadcast: Id={0}, Name={1}, Status={2}, LastModified={3}, FromNumber={4}, LocalTimeRestriction={5}, Schedules={6}, MaxActive={7}, Labels={8}]", 
                Id, Name, Status, LastModified, FromNumber, LocalTimeRestriction, Schedules, MaxActive, Labels);
        }
    }
}

