using Newtonsoft.Json;

namespace CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Find by id request with id, batchId, limit, offset and fields properties
    /// </summary>
    public class GetBroadcastCallsTextsRequest : GetByIdRequest
    {
        public long? BatchId { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [GetBroadcastCallsTextsRequest: batchId={1}]", base.ToString(), BatchId);
        }
    }
}