

namespace  CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Contains common fields for finder endpoints
    /// </summary>
    public abstract class FindRequest : CallfireModel
    {
        /// <summary>
        /// Get max number of records per page to return. If items.size() less than limit assume no more items.
        /// If value not set, default is 100
        /// </summary
        public long? Limit { get; set; }

        /// <summary>
        /// Get offset to start of page. If value not set, default is 0
        /// </summary>
        public long? Offset { get; set; }

        /// <summary>
        /// Get limit fields returned. Example fields=id,items(name,agents(id))
        ///</summary>
        public string Fields { get; set; }
    }
}