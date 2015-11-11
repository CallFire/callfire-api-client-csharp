using System;
using CallfireApiClient.Api.Webhooks.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Webhooks.Model;

namespace CallfireApiClient.Api.Webhooks
{
    public class WebhooksApi
    {
        private const string WEBHOOKS_PATH = "/webhooks";
        private const string WEBHOOKS_ITEM_PATH = "/webhooks/{}";

        private readonly RestApiClient Client;

        internal WebhooksApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all webhooks for the user.
        /// Search for webhooks on name, resource, event, callback URL, or whether they are enabled.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>paged list with webhooks</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Webhook> Find(FindWebhooksRequest request)
        {
            return Client.Get<Page<Webhook>>(WEBHOOKS_PATH, request);
        }

        /// <summary>
        /// Get webhook by id.
        /// </summary>
        /// <param name="id">id of webhook</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>webhook object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Webhook Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            String path = WEBHOOKS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<Webhook>(path, queryParams);
        }

        /// <summary>
        /// Create a Webhook for notification in the CallFire system. Use the webhooks API to receive
        /// notifications of important CallFire events. Select the resource to listen to, and then choose
        /// the events for that resource to receive notifications on. When an event triggers,
        /// a POST will be made to the callback URL with a payload of notification information.
        /// </summary>
        /// <param name="webhook">webhook to create</param>
        /// <returns>ResourceId object with id of created webhook</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(Webhook webhook)
        {
            return Client.Post<ResourceId>(WEBHOOKS_PATH, webhook);
        }

        /// <summary>
        /// Update webhook
        /// </summary>
        /// <param name="webhook">webhook to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(Webhook webhook)
        {
            string path = WEBHOOKS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, webhook.Id.ToString());
            Client.Put<object>(path, webhook);
        }

        /// <summary>
        /// Delete webhook by id
        /// </summary>
        /// <param name="id">id of webhook</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(WEBHOOKS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }
    }
}

