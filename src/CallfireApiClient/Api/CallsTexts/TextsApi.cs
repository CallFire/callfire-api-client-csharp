using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Keywords.Model.Request;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model.Request;

namespace CallfireApiClient.Api.CallsTexts
{

    public class TextsApi
    {
        private const string TEXTS_PATH = "/texts";
        private const string TEXTS_ITEM_PATH = "/texts/{}";

        private readonly RestApiClient Client;

        internal TextsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Finds all texts sent or received by the user. Use "campaignId=0"  parameter to query for all
        /// texts sent through the POST /texts API.
        /// If no limit is given then the last 100 texts will be returned.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Text> Find(FindTextsRequest request)
        {
            return Client.Get<Page<Text>>(TEXTS_PATH, request);
        }

        /// <summary>
        /// Get text by id
        /// </summary>
        /// <param name="id">id of text</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Text Get(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = TEXTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString());

            var queryParams = ClientUtils.BuildQueryParams("fields", fields);

            return Client.Get<Text>(path, queryParams);
        }

        /// <summary>
        /// Send texts to recipients through existing campaign, if null default campaign will be used
        /// Use the /texts API to quickly send individual texts.A verified Caller ID and sufficient
        /// credits are required to make a call.
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
        public IList<Text> Send(List<TextRecipient> recipients, long? campaignId = null, string fields = null)
        {
            Validate.NotBlank(recipients.ToString(), "recipients cannot be blank");
            
            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            queryParams.Add("campaignId", campaignId);
            queryParams.Add("fields", fields);

            return Client.Post<ListHolder<Text>>(TEXTS_PATH, recipients, queryParams).Items;
        }
    }
}