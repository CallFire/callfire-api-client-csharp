

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
        protected long Limit { get; set; }

        /// <summary>
        /// Get offset to start of page. If value not set, default is 0
        /// </summary>
        protected long Offset { get; set; }

        /// <summary>
        /// Get limit fields returned. Example fields=id,items(name,agents(id))
        ///</summary>
        protected string Fields { get; set; }

        /// <summary>
        /// Abstract builder for find requests
        /// <summary>/
        /// <typeparam name="B">type of builder</typeparam>
        public abstract class FindRequestBuilder<B, R> : AbstractBuilder<R>
            where B: FindRequestBuilder<B, R>
            where R: FindRequest
        {

            protected FindRequestBuilder(R request)
                : base(request)
            {
            }

            ///
            /// Set max number of items returned.
            ///
            /// <param name="limit"/> limit max number of items
            /// <returns>builder object</returns>
            ///
            public B Limit(long limit)
            {
                Request.Limit = limit;
                return (B)this;
            }

            /// <summary>
            /// Offset from start of paging source
            /// <summary>/
            /// <param name="offset">offset value</param>
            /// <returns>builder object</returns>
            public B Offset(long offset)
            {
                Request.Offset = offset;
                return (B)this;
            }

            /// <summary>
            /// Set limit fields returned. Example fields=id,items(name,agents(id))
            /// <summary>/
            /// <param name="fields">fields fields to return</param>
            /// <returns>builder object</returns>
            public B Fields(string fields)
            {
                Request.Fields = fields;
                return (B)this;
            }
        }
    }
}