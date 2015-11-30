using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;

namespace CallfireApiClient.Api.Campaigns
{
    public class BatchesApi
    {
        private const string BATCH_PATH = "/campaigns/batches/{}";
        private readonly RestApiClient Client;

        internal BatchesApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Returns a single Batch instance for a given batch id.
        /// This API is useful for determining the state of a validating batch.
        /// </summary>
        /// <param name="id">id of batch</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>requested batch</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Batch Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<Batch>(BATCH_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()), queryParams);
        }

        /// <summary>
        /// Update batch
        /// </summary>
        /// <param name="batch">batch to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(Batch batch)
        {
            Validate.NotNull(batch.Id, "batch.id");
            Client.Put<object>(BATCH_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, batch.Id.ToString()), batch);
        }
    }
}

