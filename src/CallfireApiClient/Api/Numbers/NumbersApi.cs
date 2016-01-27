using System;
using System.Collections.Generic;
using CallfireApiClient.Api.Numbers.Model;
using CallfireApiClient.Api.Numbers.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Numbers
{
    public class NumbersApi
    {
        private const string NUMBERS_LOCAL_PATH = "/numbers/local";
        private const string NUMBERS_REGIONS_PATH = "/numbers/regions";
        private const string NUMBERS_TOLLFREE_PATH = "/numbers/tollfree";

        private readonly RestApiClient Client;

        internal NumbersApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find number in local catalog by prefix, zipcode, etc...
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>available numbers in catalog</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Number> FindNumbersLocal(FindNumbersLocalRequest request)
        {
            return Client.Get<ListHolder<Number>>(NUMBERS_LOCAL_PATH, request).Items;
        }

        /// <summary>
        /// Find number region information. Use this API to obtain detailed region information that
        /// can then be used to query for more specific phone numbers than a general query.
        /// </summary>
        /// <param name="request">request object</param>
        /// <returns>paged response</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Region> FindNumberRegions(FindNumberRegionsRequest request)
        {
            return Client.Get<Page<Region>>(NUMBERS_REGIONS_PATH, request);
        }

        /// <summary>
        /// Find numbers in the CallFire tollfree numbers catalog that are available for purchase.
        /// </summary>
        /// <param name="request">request object</param>
        /// <returns>list of numbers</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Number> FindNumbersTollfree(CommonFindRequest request)
        {
            return Client.Get<ListHolder<Number>>(NUMBERS_TOLLFREE_PATH, request).Items;
        }
    }
}

