
namespace CallfireApiClient.Api.Common.Model.Request {
/// <summary>
/// Common find request with limit, offset and fields properties
/// </summary>
public class CommonFindRequest : FindRequest {

    private CommonFindRequest() {
    }

    public static Builder create() {
        return new Builder();
    }

    /// <summary>
    /// Request builder
    /// </summary
    public static class Builder : FindRequestBuilder<Builder, CommonFindRequest> {

        private Builder() {
            super(new CommonFindRequest());
        }
    }
}
