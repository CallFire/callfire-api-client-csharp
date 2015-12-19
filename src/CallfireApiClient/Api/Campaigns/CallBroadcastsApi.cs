using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Campaigns
{
    public class CallBroadcastsApi
    {
        private const string CB_PATH = "/calls/broadcasts";
        private const string CB_ITEM_PATH = "/callls/broadcasts/{}";
        private const string CB_ITEM_BATCHES_PATH = "/calls/broadcasts/{}/batches";
        private const string CB_ITEM_CALLS_PATH = "/calls/broadcasts/{}/calls";
        private const string CB_ITEM_START_PATH = "/calls/broadcasts/{}/start";
        private const string CB_ITEM_STOP_PATH = "/calls/broadcasts/{}/stop";
        private const string CB_ITEM_ARCHIVE_PATH = "/calls/broadcasts/{}/archive";
        private const string CB_ITEM_STATS_PATH = "/calls/broadcasts/{}/stats";
        private const string CB_ITEM_RECIPIENTS_PATH = "/calls/broadcasts/{}/recipients";
        private const string CB_ITEM_RECIPIENTS_FILE_PATH = "/calls/broadcasts/{}/recipients-file";

        private readonly RestApiClient Client;

        internal CallBroadcastsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all call broadcasts created by the user. Can query on label, name, and the current
        /// running status of the campaign.
        /// </summary>
        /// <param name="request">request object with filtering options</param>
        /// <returns>page with CallBroadcast objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<CallBroadcast> Find(FindBroadcastsRequest request)
        {
            return Client.Get<Page<CallBroadcast>>(CB_PATH, request);
        }

        /// <summary>
        /// Create a call broadcast campaign using the Call Broadcast API. A campaign can be created with
        /// no contacts and bare minimum configuration, but contacts will have to be added further on to use the campaign.
        /// If start set to true campaign starts immediately
        /// </summary>
        /// <param name="broadcast">call broadcast to create</param>
        /// <param name="start">if set to true then broadcast will start immediately, by default it set to false</param>
        /// <returns>ResourceId object with id of created broadcast</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(CallBroadcast broadcast, bool start = false)
        {
            var queryParams = ClientUtils.BuildQueryParams("start", start.ToString());
            return Client.Post<ResourceId>(CB_PATH, broadcast, queryParams);
        }

        /// <summary>
        /// Get call broadcast by id
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
        public CallBroadcast Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            string path = CB_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<CallBroadcast>(path, queryParams);
        }

        /// <summary>
        /// Update broadcast
        /// </summary>
        /// <param name="broadcast">broadcast to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(CallBroadcast broadcast)
        {
            Validate.NotNull(broadcast.Id, "broadcast.id cannot be null");
            Client.Put<object>(CB_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, broadcast.Id.ToString()), broadcast);
        }

        /// <summary>
        /// Starts call broadcast
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
            Client.Post<object>(CB_ITEM_START_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Stops call broadcast
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
            Client.Post<object>(CB_ITEM_STOP_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Archives call broadcast
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
            Client.Post<object>(CB_ITEM_ARCHIVE_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Get call broadcast batches. Retrieve batches associated with call campaign
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
            String path = CB_ITEM_BATCHES_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Batch>>(path, request);
        }

        /// <summary>
        /// Add batch to call broadcast.
        /// The add batch API allows the user to add additional batches to an already created call broadcast
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
            String path = CB_ITEM_BATCHES_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.CampaignId.ToString());
            return Client.Post<ResourceId>(path, request);
        }

        /// <summary>
        /// Get calls associated with call broadcast ordered by date
        /// </summary>
        /// <param name="request">request with properties to filter</param>
        /// <returns>calls assosiated with broadcast</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Call> GetCalls(GetByIdRequest request)
        {
            String path = CB_ITEM_CALLS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Call>>(path, request);
        }

        /// <summary>
        /// Get statistics on call broadcast
        /// </summary>
        /// <param name="id">id of call broadcast</param>
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
        public CallBroadcastStats GetStats(long id, string fields = null, DateTime? begin = null, DateTime? end = null)
        {
            var queryParams = new List<KeyValuePair<string, object>>(3);
            ClientUtils.AddQueryParamIfSet("fields", fields, queryParams);
            ClientUtils.AddQueryParamIfSet("begin", begin, queryParams);
            ClientUtils.AddQueryParamIfSet("end", end, queryParams);
            String path = CB_ITEM_STATS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<CallBroadcastStats>(path, queryParams);
        }

        /// <summary>
        /// Use this API to add recipients to an already created call broadcast. Post a list of Recipient
        /// objects for them to be immediately added to the call broadcast campaign. These contacts do not
        /// go through validation process, and will be acted upon as they are added. Recipients may be added
        /// as a list of contact ids, or list of numbers.
        /// </summary>
        /// <param name="id">id of call broadcast</param>
        /// <param name="recipients">recipients to add</param>
        /// <param name="fields">limit fields returned. E.g. fields=id,name or fields=items(id,name)</param>
        /// <returns>Call objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Call> AddRecipients(long id, IList<Recipient> recipients, String fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            string path = CB_ITEM_RECIPIENTS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Post<ListHolder<Call>>(path, recipients, queryParams).Items;
        }
    }
}

