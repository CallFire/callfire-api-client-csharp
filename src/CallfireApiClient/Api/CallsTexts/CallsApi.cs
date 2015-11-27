using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model.Request;

namespace CallfireApiClient.Api.CallsTexts
{

    public class CallsApi
    {
        private const string CALLS_PATH = "/calls";
        private const string CALLS_ITEM_PATH = "/calls/{}";

        private readonly RestApiClient Client;

        internal CallsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Finds all calls sent or received by the user, filtered by different properties, broadcast id,
        /// toNumber, fromNumber, label, state, etc.Use "campaignId=0" parameter to query
        /// for all calls sent through the POST /calls API {@link CallsApi#send(List)}.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Call> Find(FindCallsRequest request)
        {
            return Client.Get<Page<Call>>(CALLS_PATH, request);
        }

        /// <summary>
        /// Get call by id
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Call Get(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = CALLS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString());

            var queryParams = ClientUtils.BuildQueryParams("fields", fields);

            return Client.Get<Call>(path, queryParams);
        }

        /// <summary>
        /// Send calls to recipients through default campaign.
        /// Use the API to quickly send individual calls.
        /// A verified Caller ID and sufficient credits are required to make a call.
        /// </summary>
        /// <param name="recipients">call recipients</param>
        /// <param name="campaignId">specify a campaignId to send calls quickly on a previously created campaign</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Call> Send(IList<CallRecipient> recipients, long? campaignId = null, string fields = null)
        {
            Validate.NotBlank(recipients.ToString(), "recipients cannot be blank");
            
            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            queryParams.Add("campaignId", campaignId);
            queryParams.Add("fields", fields);

            return Client.Post<ListHolder<Call>>(CALLS_PATH, recipients, queryParams).Items;
        }
    }
}