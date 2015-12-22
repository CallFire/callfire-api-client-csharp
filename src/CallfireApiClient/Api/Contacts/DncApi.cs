using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts
{
    public class DncApi
    {
        private const string DNC_PATH = "/contacts/dncs";

        private readonly RestApiClient Client;

        internal DncApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all Do Not Contact (DNC) objects created by the user.
        /// These DoNotContact entries only affect calls/texts/campaigns on this account.
        /// </summary>
        /// <param name="request">find request with different properties to filter</param>
        /// <returns>paged list with dnc objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<DoNotContact> Find(FindDncContactsRequest request)
        { 
            return Client.Get<Page<DoNotContact>>(DNC_PATH, request);
        }

        /// <summary>
        /// Update a Do Not Contact (DNC) contact value. Can toggle whether the DNC is enabled for calls/texts.
        /// </summary>
        /// <param name="dnc">DNC item to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(DoNotContact dnc)
        {
            Client.Put<DoNotContact>(DNC_PATH, dnc);
        }
    }
}