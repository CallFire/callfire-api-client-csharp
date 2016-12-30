using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.CallsTexts.Model.Request
{
    public class FindSoundsRequest : FindRequest
    {
        public string Filter { get; set; }

        public bool? IncludeArchived { get; set; }

        public bool? IncludePending { get; set; }

        public bool? IncludeScrubbed { get; set; }

        public override string ToString()
        {
            return string.Format("[FindSoundsRequest: {0}, Filter={1}, IncludeArchived={2}, IncludePending={3}, IncludeScrubbed={4}]", base.ToString(), Filter, IncludeArchived, IncludePending, IncludeScrubbed);
        }
    }
}

