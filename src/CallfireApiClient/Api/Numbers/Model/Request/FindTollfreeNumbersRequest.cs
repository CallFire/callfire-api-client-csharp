using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Numbers.Model.Request
{
    /// <summary>
    /// Contains fields for getting tollfree numbers
    /// </summary>
    public class FindTollfreeNumbersRequest : CallfireModel
    {
        /// <summary>
        /// Filter toll free numbers by prefix, pattern must be 3 char long and should end with ''. Examples: 8**, 85, 87 (but 855 will fail because pattern must end with '').
        ///</summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Get max number of records per page to return. If items.size() less than limit assume no more items.
        /// If value not set, default is 100
        /// </summary
        public long? Limit { get; set; }

        /// <summary>
        /// Get limit fields returned. Example fields=id,items(name,agents(id))
        ///</summary>
        public string Fields { get; set; }


        public override string ToString()
        {
            return string.Format("[FindRequest: Pattern={0}, Limit={1}, Fields={2}]", Pattern, Limit, Fields);
        }
    }
}