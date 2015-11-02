
namespace CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Common find request with limit, offset and fields properties
    /// </summary>
    public class CommonFindRequest : FindRequest
    {
        CommonFindRequest()
        {
        }

        public static Builder Create()
        {
            return new Builder();
        }

        /// <summary>
        /// Request builder
        /// </summary>
        public class Builder : FindRequestBuilder<Builder, CommonFindRequest>
        {
            internal Builder()
                : base(new CommonFindRequest())
            {
            }
        }
    }
}