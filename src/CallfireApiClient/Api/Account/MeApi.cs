using System;
using CallfireApiClient.Api.Account.Model;
using System.Collections.Specialized;

namespace CallfireApiClient.Api.Account
{
    public class MeApi
    {
        const string ME_ACCOUNT_PATH = "/me/account";
        readonly RestApiClient Client;

        internal MeApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find account details for the user. Details include name, email, and basic account permissions.
        /// GET /me/account
        /// </summary>
        /// <returns>user's account</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Model.Account GetAccount()
        {
            return Client.Get<Model.Account>(ME_ACCOUNT_PATH);
        }
    }
}

