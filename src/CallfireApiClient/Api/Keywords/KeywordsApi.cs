using System;
using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Keywords
{

    public class KeywordsApi
    {
        private const string KEYWORDS_PATH = "/keywords";
        private const string KEYWORD_AVAILABLE_PATH = "/keywords/{}/available";

        private readonly RestApiClient Client;

        internal KeywordsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find keyword objects by list of keyword names
        /// </summary>
        /// <param name="keywords">list of keyword names</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Keyword> Find(IList<string> keywords)
        {
            List<KeyValuePair<string, object>> queryParams = new List<KeyValuePair<string, object>>();
            ClientUtils.AddQueryParamIfSet("keywords", keywords, queryParams);
            return Client.Get<ListHolder<Keyword>>(KEYWORDS_PATH, queryParams).Items;
        }

        /// <summary>
        /// Find an individual keyword for purchase on the CallFire platform.
        /// </summary>
        /// <param name="keyword">keyword name</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Boolean IsAvailable(string keyword)
        {
            return Client.Get<Boolean>(KEYWORD_AVAILABLE_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, keyword));
        }


    }
}