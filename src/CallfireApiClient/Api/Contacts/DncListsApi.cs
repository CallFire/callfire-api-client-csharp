using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts
{

    public class DncListsApi
    {
        private const string DNC_LISTS_PATH = "/contacts/dncs/lists";
        private const string DNC_LISTS_UNIVERSAL_PATH = "/contacts/dncs/lists/universal/{}";
        private const string DNC_LISTS_LIST_PATH = "/contacts/dncs/lists/{}";
        private const string DNC_LISTS_LIST_ITEMS_PATH = "/contacts/dncs/lists/{}/items";
        private const string DNC_LISTS_LIST_ITEMS_NUMBER_PATH = "/contacts/dncs/lists/{}/items/{}";
       

        private readonly RestApiClient Client;

        internal DncListsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find do not contact (DNC) lists
        /// </summary>
        /// <param name="request">request with properties to find</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<DncList> Find(FindDncListsRequest request)
        { 
            return Client.Get<Page<DncList>>(DNC_LISTS_PATH, request);
        }


        /// <summary>
        /// Create do not contact (DNC) list.
        /// </summary>
        /// <param name="dncList">list to create</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(DncList dncList)
        {
            return Client.Post<ResourceId>(DNC_LISTS_PATH, dncList);
        }


        /// <summary>
        /// Search Universal Do Not Contact by number
        /// </summary>
        /// <param name="toNumber">Phone Number in Do Not Contact list</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<UniversalDnc> GetUniversalDncNumber(string toNumber)
        {
            return GetUniversalDncNumber(toNumber, null, null);
        }


        /// <summary>
        /// Search Universal Do Not Contact by number
        /// </summary>
        /// <param name="toNumber">Phone Number in Do Not Contact list</param>
        /// <param name="fromNumber">Searches for entries where fromNumber is communicating with toNumber, or vice versa.</param>
        /// <param name="fields">Limit fields returned. Example fields=limit,offset,items(id,name)</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<UniversalDnc> GetUniversalDncNumber(string toNumber, string fromNumber, string fields)
        {
            Validate.NotBlank(toNumber, "toNumber cannot be blank");
            string path = DNC_LISTS_UNIVERSAL_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    toNumber);

            Dictionary<string, object> queryParams = new Dictionary<string, object>();
            queryParams.Add("fromNumber", fromNumber);
            queryParams.Add("fields", fields);
            return Client.Get<ListHolder<UniversalDnc>>(path, queryParams).Items;
        }


        /// <summary>
        /// Get DNC list by id
        /// </summary>
        /// <param name="id">id of DNC list</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public DncList Get(long id)
        {
            return Get(id, null);
        }


        /// <summary>
        /// Find do not contact (DNC) lists
        /// </summary>
        /// <param name="id">id of DNC list</param>
        /// <param name="fields">limit fields returned. Example fields=name,status</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public DncList Get(long id, string fields)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = DNC_LISTS_LIST_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString());

            var queryParams = ClientUtils.BuildQueryParams("fields", fields);

            return Client.Get<DncList>(path, queryParams);
        }


        /// <summary>
        /// Delete DNC list
        /// </summary>
        /// <param name="id">DNC list id</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(DNC_LISTS_LIST_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString()));
        }


        /// <summary>
        /// Get DNC list items
        /// </summary>
        /// Property <b>request.id</b> required
        /// <param name="request">request object with properties to filter</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<DoNotContact> GetListItems(GetByIdRequest request)
        {
            Validate.NotBlank(request.Id.ToString(), "request.id cannot be null");
            string path = DNC_LISTS_LIST_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    request.Id.ToString());
            return Client.Get<Page<DoNotContact>>(path, request);
        }

        /// <summary>
        /// Add DNC list items to list
        /// </summary>
        /// <param name="request">request object with DNC items to add</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void AddListItems(AddDncListItemsRequest<DoNotContact> request)
        {
            Validate.NotBlank(request.contactListId.ToString(), "request.contactListId cannot be null");
            string path = DNC_LISTS_LIST_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    request.contactListId.ToString());
            Client.Post<object>(path, request.contacts);
        }

        /// <summary>
        /// Delete single DNC list contact by number
        /// </summary>
        /// <param name="id">id of DNC list</param>
        /// <param name="number">number to remove</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void RemoveListItem(long id, string number)
        {
            Validate.NotBlank(number, "number cannot be blank");
            string path = DNC_LISTS_LIST_ITEMS_NUMBER_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString()).ReplaceFirst(ClientConstants.PLACEHOLDER, number);
            Client.Delete(path);
        }


        /// <summary>
        /// Delete DNC list items
        /// </summary>
        /// <param name="id">id of DNC list</param>
        /// <param name="numbers">numbers to remove</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void RemoveListItems(long id, IList<string> numbers)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = DNC_LISTS_LIST_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString());
            List<KeyValuePair<string, object>> queryParams = new List<KeyValuePair<string, object>>(1);
            ClientUtils.AddQueryParamIfSet("number", numbers, queryParams);
            Client.Delete(path, queryParams);
        }
    
    }
}