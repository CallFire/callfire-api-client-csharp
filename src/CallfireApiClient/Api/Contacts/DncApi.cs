using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts
{
    public class DncApi
    {

        private const string DNC_PATH = "/contacts/dncs";
        private const string DNC_SOURCES_PATH = "/contacts/dncs/sources/{}";
        private const string UNIVERSAL_DNC_PATH = "/contacts/dncs/universals/{}";
        private const string DNC_NUMBER_PATH = "/contacts/dncs/{}";

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
        public Page<DoNotContact> Find(FindDncNumbersRequest request)
        { 
            return Client.Get<Page<DoNotContact>>(DNC_PATH, request);
        }

        /// <summary>
        /// Get do not contact(dnc).
        /// </summary>
        /// <param name="number">number to get dnc for</param>
        /// <returns>dnc object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public DoNotContact Get(string number)
        {
            Validate.NotNull(number, "number");
            string path = DNC_NUMBER_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, number);
            return Client.Get<DoNotContact>(path);
        }

        /// <summary>
        /// Add Do Not Contact (DNC) entries.
        /// </summary>
        /// <param name="request">DNC items to create</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Create(CreateDncsRequest request)
        {
            Client.Post<object>(DNC_PATH, request);
        }

        /// <summary>
        /// Update a Do Not Contact (DNC) value. Can toggle whether the DNC is enabled for calls/texts.
        /// </summary>
        /// <param name="request">DNC update request</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(UpdateDncRequest request)
        {
            Validate.NotNull(request.Number, "number");
            string path = DNC_NUMBER_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Number);
            Client.Put<object>(path, request);
        }

        /// <summary>
        /// Delete a Do Not Contact(DNC) value.
        /// </summary>
        /// <param name="number">number to remove dnc for</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(string number)
        {
            Validate.NotNull(number, "number");
            string path = DNC_NUMBER_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, number);
            Client.Delete(path);
        }

        /// <summary>
        /// Find universal do not contacts(udnc) associated with toNumber
        /// </summary>
        /// <param name="request">find request with different properties to filter</param>
        /// <returns>list with universal dnc objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<UniversalDnc> FindUniversalDncs(FindUniversalDncsRequest request)
        {
            Validate.NotNull(request.ToNumber, "toNumber");
            string path = UNIVERSAL_DNC_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.ToNumber);
            return Client.Get<ListHolder<UniversalDnc>>(path, request).Items;
        }

        /// <summary>
        /// Delete do not contact (dnc) numbers contained in source.
        /// </summary>
        /// <param name="number">Source associated with Do Not Contact (DNC) entry.</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void DeleteDncsFromSource(string source)
        {
            Validate.NotNull(source, "source");
            string path = DNC_SOURCES_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, source);
            Client.Delete(path);
        }

    }
}
