using System;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Webhooks.Model;
using CallfireApiClient.Api.Webhooks.Model.Request;

namespace CallfireApiClient.Api.Webhooks
{
    public class SubscriptionsApi
    {
        private const string SUBSCRIPTIONS_PATH = "/subscriptions";
        private const string SUBSCRIPTIONS_ITEM_PATH = "/subscriptions/{}";

        private readonly RestApiClient Client;

        internal SubscriptionsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all subscriptions for the user.
        /// Search for subscriptions on campaign id, resource, event, from number, to number, or whether they are enabled.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>paged list with subscriptions</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Subscription> Find(FindSubscriptionsRequest request)
        {
            return Client.Get<Page<Subscription>>(SUBSCRIPTIONS_PATH, request);
        }

        /// <summary>
        /// Get subscription by id.
        /// </summary>
        /// <param name="id">id of subscription</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>subscription object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Subscription Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            var path = SUBSCRIPTIONS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<Subscription>(path, queryParams);
        }

        /// <summary>
        /// Create a Subscription for notification in the CallFire system. Use the subscriptions API to receive
        /// notifications of important CallFire events. Select the resource to listen to, and then choose
        /// the events for that resource to receive notifications on. When an event triggers,
        /// a POST will be made to the callback URL with a payload of notification information.
        /// </summary>
        /// <param name="subscription">subscription to create</param>
        /// <returns>ResourceId object with id of created subscription</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(Subscription subscription)
        {
            return Client.Post<ResourceId>(SUBSCRIPTIONS_PATH, subscription);
        }

        /// <summary>
        /// Update subscription
        /// </summary>
        /// <param name="subscription">subscription to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(Subscription subscription)
        {
            string path = SUBSCRIPTIONS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, subscription.Id.ToString());
            Client.Put<object>(path, subscription);
        }

        /// <summary>
        /// Delete subscription by id
        /// </summary>
        /// <param name="id">id of subscription</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(SUBSCRIPTIONS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }
    }
}

