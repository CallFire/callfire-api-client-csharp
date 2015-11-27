using Newtonsoft.Json;

namespace CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Find by id request with id, limit, offset and fields properties
    /// </summary>
    public class GetByIdRequest : FindRequest
    {
        [JsonIgnore]
        public long? Id { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [GetByIdRequest: Id={1}]", base.ToString(), Id);
        }
    }
}