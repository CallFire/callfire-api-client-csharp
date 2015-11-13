using System;
using CallfireApiClient.Api.Numbers.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Numbers.Model;

namespace CallfireApiClient.Api.Numbers
{
    public class NumberLeasesApi
    {
        private const string NUMBER_LEASES_PATH = "/numbers/leases";
        private const string NUMBER_LEASES_ITEM_PATH = "/numbers/leases/{}";
        private const string NUMBER_CONFIGS_PATH = "/numbers/leases/configs";
        private const string NUMBER_CONFIGS_ITEM_PATH = "/numbers/leases/configs/{}";

        private readonly RestApiClient Client;

        internal NumberLeasesApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find number leases by prefix, zipcode, etc...
        /// This API is useful for finding all numbers currently owned by an account.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>paged leases</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<NumberLease> Find(FindNumberLeasesRequest request)
        {
            return Client.Get<Page<NumberLease>>(NUMBER_LEASES_PATH, request);
        }

        /// <summary>
        /// Get number lease by number
        /// </summary>
        /// <param name="number">leased phone number</param>
        /// <param name="fields">Limit fields returned. Example fields=id,name</param>
        /// <returns>object which represents number lease</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public NumberLease Get(string number, string fields = null)
        {
            Validate.NotBlank(number, "number cannot be blank");
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<NumberLease>(NUMBER_LEASES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, number), queryParams);
        }

        /// <summary>
        /// Update number lease
        /// </summary>
        /// <param name="lease">lease to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(NumberLease lease)
        {
            Validate.NotBlank(lease.PhoneNumber, "lease.number cannot be blank");
            Client.Put<object>(NUMBER_LEASES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, lease.PhoneNumber), lease);
        }

        /// <summary>
        /// Find all number lease configs for the user.
        /// </summary>
        /// <param name="request">request to filter</param>
        /// <returns>paged number configs</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<NumberConfig> FindConfigs(FindNumberLeaseConfigsRequest request)
        {
            return Client.Get<Page<NumberConfig>>(NUMBER_CONFIGS_PATH, request);
        }

        /// <summary>
        /// Get number lease config
        /// </summary>
        /// <param name="number">leased phone number</param>
        /// <param name="fields">Limit fields returned. Example fields=id,name</param>
        /// <returns>object which represents number lease config</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public NumberConfig GetConfig(string number, string fields = null)
        {
            Validate.NotBlank(number, "number cannot be blank");
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<NumberConfig>(NUMBER_CONFIGS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, number), queryParams);
        }

        /// <summary>
        /// Update number lease config
        /// </summary>
        /// <param name="config">config to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void UpdateConfig(NumberConfig config)
        {
            Validate.NotBlank(config.Number, "config.number cannot be blank");
            Client.Put<object>(NUMBER_CONFIGS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, config.Number), config);
        }
    }
}

