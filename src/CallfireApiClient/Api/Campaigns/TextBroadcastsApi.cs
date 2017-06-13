using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Campaigns
{
    public class TextBroadcastsApi
    {
        private const string TB_PATH = "/texts/broadcasts";
        private const string TB_BATCHES_ITEM_PATH = "/texts/broadcasts/batches/{}";
        private const string TB_ITEM_PATH = "/texts/broadcasts/{}";
        private const string TB_ITEM_BATCHES_PATH = "/texts/broadcasts/{}/batches";
        private const string TB_ITEM_TEXTS_PATH = "/texts/broadcasts/{}/texts";
        private const string TB_ITEM_START_PATH = "/texts/broadcasts/{}/start";
        private const string TB_ITEM_STOP_PATH = "/texts/broadcasts/{}/stop";
        private const string TB_ITEM_ARCHIVE_PATH = "/texts/broadcasts/{}/archive";
        private const string TB_ITEM_STATS_PATH = "/texts/broadcasts/{}/stats";
        private const string TB_ITEM_RECIPIENTS_PATH = "/texts/broadcasts/{}/recipients";
        private const string TB_ITEM_RECIPIENTS_FILE_PATH = "/texts/broadcasts/{}/recipients-file";

        private readonly RestApiClient Client;

        internal TextBroadcastsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all text broadcasts created by the user. Can query on label, name, and the current
        /// running status of the campaign.
        /// </summary>
        /// <param name="request">request object with filtering options</param>
        /// <returns>page with TextBroadcast objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<TextBroadcast> Find(FindBroadcastsRequest request)
        {
            return Client.Get<Page<TextBroadcast>>(TB_PATH, request);
        }

        /// <summary>
        /// Create a text broadcast campaign using the Text Broadcast API. A campaign can be created with
        /// no contacts and bare minimum configuration, but contacts will have to be added further on to use the campaign.
        /// If start set to true campaign starts immediately
        /// </summary>
        /// <param name="broadcast">text broadcast to create</param>
        /// <param name="start">if set to true then broadcast will start immediately, by default it set to false</param>
        /// <param name="strictValidation">apply strict validation for contacts</param>
        /// <returns>ResourceId object with id of created broadcast</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(TextBroadcast broadcast, bool start = false, bool? strictValidation = null)
        {
            var queryParams = new List<KeyValuePair<string, object>>(2);
            ClientUtils.AddQueryParamIfSet("start", start.ToString(), queryParams);
            ClientUtils.AddQueryParamIfSet("strictValidation", strictValidation.ToString(), queryParams);
            return Client.Post<ResourceId>(TB_PATH, broadcast, queryParams);
        }

        /// <summary>
        /// Get text broadcast by id
        /// </summary>
        /// <param name="id">id of broadcast</param>
        /// <param name="fields">limit fields returned. Example fields=id,message</param>
        /// <returns>broadcast object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public TextBroadcast Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            string path = TB_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<TextBroadcast>(path, queryParams);
        }

        /// <summary>
        /// Update broadcast
        /// </summary>
        /// <param name="broadcast">broadcast to update</param>
        /// <param name="strictValidation">apply strict validation for contacts</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(TextBroadcast broadcast, bool? strictValidation = null)
        {
            Validate.NotNull(broadcast.Id, "broadcast.id cannot be null");
            var queryParams = ClientUtils.BuildQueryParams("strictValidation", strictValidation.ToString());
            Client.Put<object>(TB_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, broadcast.Id.ToString()), broadcast, queryParams);
        }

        /// <summary>
        /// Starts text broadcast
        /// </summary>
        /// <param name="id">id of broadcast</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Start(long id)
        {
            Client.Post<object>(TB_ITEM_START_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Stops text broadcast
        /// </summary>
        /// <param name="id">id of broadcast</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Stop(long id)
        {
            Client.Post<object>(TB_ITEM_STOP_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Archives text broadcast
        /// </summary>
        /// <param name="id">id of broadcast</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Archive(long id)
        {
            Client.Post<object>(TB_ITEM_ARCHIVE_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Get text broadcast batches. Retrieve batches associated with text campaign
        /// </summary>
        /// <param name="request">get request</param>
        /// <returns>broadcast batches</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Batch> GetBatches(GetByIdRequest request)
        {
            String path = TB_ITEM_BATCHES_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Batch>>(path, request);
        }

        /// <summary>
        /// Add batch to text broadcast.
        /// The add batch API allows the user to add additional batches to an already created text broadcast
        /// campaign. The added batch will go through the CallFire validation process, unlike in the
        /// recipients version of this API. Because of this, use the scrubDuplicates flag to remove duplicates
        /// from your batch. Batches may be added as a contact list id, a list of contact ids, or a list of numbers.
        /// </summary>
        /// <param name="request">request with contacts</param>
        /// <returns>id of created batch</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId AddBatch(AddBatchRequest request)
        {
            String path = TB_ITEM_BATCHES_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.CampaignId.ToString());
            var queryParams = ClientUtils.BuildQueryParams("strictValidation", request.StrictValidation.ToString());
            return Client.Post<ResourceId>(path, request, queryParams);
        }

        /// <summary>
        /// Get texts associated with text broadcast ordered by date
        /// </summary>
        /// <param name="request">request with properties to filter</param>
        /// <returns>texts assosiated with broadcast</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        [Obsolete]
        public Page<Text> GetTexts(GetByIdRequest request)
        {
            String path = TB_ITEM_TEXTS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Text>>(path, request);
        }

        /// <summary>
        /// Get texts associated with text broadcast ordered by date
        /// </summary>
        /// <param name="request">request with properties to filter</param>
        /// <returns>texts assosiated with broadcast</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Text> GetTexts(GetBroadcastCallsTextsRequest request)
        {
            String path = TB_ITEM_TEXTS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Text>>(path, request);
        }

        /// <summary>
        /// Get statistics on text broadcast
        /// </summary>
        /// <param name="id">id of text broadcast</param>
        /// <param name="fields">limit fields returned. E.g. fields=id,name or fields=items(id,name)</param>
        /// <param name="begin">begin date to filter</param>
        /// <param name="end">end date to filter</param>
        /// <returns>broadcast stats object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public TextBroadcastStats GetStats(long id, string fields = null, DateTime? begin = null, DateTime? end = null)
        {
            var queryParams = new List<KeyValuePair<string, object>>(3);
            ClientUtils.AddQueryParamIfSet("fields", fields, queryParams);
            ClientUtils.AddQueryParamIfSet("begin", begin, queryParams);
            ClientUtils.AddQueryParamIfSet("end", end, queryParams);
            String path = TB_ITEM_STATS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<TextBroadcastStats>(path, queryParams);
        }

        /// <summary>
        /// Use this API to add recipients to an already created text broadcast. Post a list of Recipient
        /// objects for them to be immediately added to the text broadcast campaign. These contacts do not
        /// go through validation process, and will be acted upon as they are added. Recipients may be added
        /// as a list of contact ids, or list of numbers.
        /// </summary>
        /// <param name="id">id of text broadcast</param>
        /// <param name="recipients">recipients to add</param>
        /// <param name="fields">limit fields returned. E.g. fields=id,name or fields=items(id,name)</param>
        /// <param name="strictValidation">apply strict validation for contacts</param>
        /// <returns>Text objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Text> AddRecipients(long id, IList<TextRecipient> recipients, String fields = null, bool? strictValidation = null)
        {
            var queryParams = new List<KeyValuePair<string, object>>(2);
            ClientUtils.AddQueryParamIfSet("fields", fields, queryParams);
            ClientUtils.AddQueryParamIfSet("strictValidation", strictValidation, queryParams);
            string path = TB_ITEM_RECIPIENTS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Post<ListHolder<Text>>(path, recipients, queryParams).Items;
        }
    }
}

