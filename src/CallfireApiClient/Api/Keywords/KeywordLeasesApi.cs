using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Keywords
{
    public class KeywordLeasesApi
    {
        private const string KEYWORD_LEASES_PATH = "/keywords/leases";
        private const string KEYWORD_LEASES_ITEM_PATH = "/keywords/leases/{}";

        private readonly RestApiClient Client;

        internal KeywordLeasesApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all owned keyword leases for a user. A keyword lease is the ownership information involving a keyword.
        /// </summary>
        /// <param name="request">request payload</param>
        /// <returns>paged list with keyword lease objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<KeywordLease> Find(CommonFindRequest request)
        {
            return Client.Get<Page<KeywordLease>>(KEYWORD_LEASES_PATH, request);
        }

        /// <summary>
        /// Get keyword lease by keyword
        /// </summary>
        /// <param name="keyword">leased keyword</param>
        /// <param name="fields">Limit fields returned. Example fields=id,name</param>
        /// <returns>keyword lease object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public KeywordLease Get(string keyword, string fields = null)
        {
            Validate.NotBlank(keyword, "keyword cannot be blank");
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<KeywordLease>(KEYWORD_LEASES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    keyword.ToString()), queryParams);
        }

        /// <summary>
        /// Update keyword lease
        /// </summary>
        /// <param name="keywordLease">keyword lease payload</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(KeywordLease keywordLease)
        {
            Validate.NotBlank(keywordLease.KeywordName, "keyword in keywordLease cannot be null");
            string path = KEYWORD_LEASES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, keywordLease.KeywordName);
            Client.Put<object>(path, keywordLease);
        }

    }
}