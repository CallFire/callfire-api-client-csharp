using System;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Keywords.Model.Request;
using CallfireApiClient.Api.Numbers.Model.Request;
using CallfireApiClient.Api.Account.Model;

namespace CallfireApiClient.Api.Account
{
    public class OrdersApi
    {
        private const string ORDERS_KEYWORDS = "/orders/keywords";
        private const string ORDERS_NUMBERS = "/orders/numbers";
        private const string ORDERS_GET_ORDER = "/orders/{}";
        
        private readonly RestApiClient Client;

        internal OrdersApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Purchase keywords. Send a list of available keywords into this API to purchase them
        /// using CallFire credits. Be sure the account has credits before trying to purchase.
        /// GET /me/account
        /// </summary>
        /// <param name="request">request payload</param>
        /// <returns>ResourceId with id of created order</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId OrderKeywords(KeywordPurchaseRequest request)
        {
            return Client.Post<ResourceId>(ORDERS_KEYWORDS, request);
        }

        /// <summary>
        /// Purchase numbers. There are many ways to purchase a number. Set either tollFreeCount or localCount
        /// along with some querying fields to purchase numbers by bulk query. Set the list of numbers
        /// to purchase by list. Available numbers will be purchased using CallFire credits owned by the user.
        /// Be sure the account has credits before trying to purchase.
        /// GET /me/account
        /// </summary>
        /// <param name="request">request payload</param>
        /// <returns>ResourceId with id of created order</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId OrderNumbers(NumberPurchaseRequest request)
        {
            return Client.Post<ResourceId>(ORDERS_NUMBERS, request);
        }

        /// <summary>
        /// Get order for keyword and/or number orders
        /// GET /me/account
        /// </summary>
        /// <param name="id">id of order</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>ResourceId with id of created order</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public NumberOrder GetOrder(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<NumberOrder>(ORDERS_GET_ORDER.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()), queryParams);
        }
    }
}

