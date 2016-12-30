using System;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts
{
    public class ContactsApi
    {
        private const string CONTACTS_PATH = "/contacts";
        private const string CONTACTS_ITEM_PATH = "/contacts/{}";
        private const string CONTACTS_ITEM_HISTORY_PATH = "/contacts/{}/history";

        private readonly RestApiClient Client;

        internal ContactsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find contacts by id, contact list, or on any property name. Returns a paged list of contacts.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>paged list with contact objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Contact> Find(FindContactsRequest request)
        {
            return Client.Get<Page<Contact>>(CONTACTS_PATH, request);
        }

        /// <summary>
        /// Create contacts in the CallFire system. These contacts are not validated on creation.
        /// They will be validated upon being added to a campaign.
        /// </summary>
        /// <param name="contacts">contacts to create</param>
        /// <returns>list of ids newly created contacts</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<ResourceId> Create(IList<Contact> contacts)
        {
            return Client.Post<ListHolder<ResourceId>>(CONTACTS_PATH, contacts).Items;
        }

        /// <summary>
        /// Get contact by id. Deleted contacts can still be retrieved but will be marked deleted
        /// and will not show up when quering contacts.
        /// </summary>
        /// <param name="id">id of contact</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>contact object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Contact Get(long id, String fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<Contact>(CONTACTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()), queryParams);
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact">contact to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(Contact contact)
        {
            Client.Put<object>(CONTACTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, contact.Id.ToString()), contact);
        }

        /// <summary>
        /// Delete contact by id. This does not actually delete the contact, it just removes the contact from
        /// any contact lists and marks the contact as deleted so won't show up in queries anymore.
        /// </summary>
        /// <param name="id">id of contact</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(CONTACTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Find all texts and calls attributed to a contact.
        /// </summary>
        /// <param name="request">request to get particular contact's history</param>
        /// <returns>a list of calls and texts a contact has been involved with.</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        [Obsolete]
        public ContactHistory GetHistory(GetByIdRequest request)
        {
            String path = CONTACTS_ITEM_HISTORY_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<ContactHistory>(path, request);
        }

        /// <summary>
        /// Find all texts and calls attributed to a contact.
        /// </summary>
        /// <param name="request">request to get particular contact's history</param>
        /// <returns>a list of calls and texts a contact has been involved with.</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ContactHistory GetHistory(long id, long? limit = null, long? offset = null)
        {
            String path = CONTACTS_ITEM_HISTORY_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = new List<KeyValuePair<string, object>>(2);
            ClientUtils.AddQueryParamIfSet("limit", limit, queryParams);
            ClientUtils.AddQueryParamIfSet("offset", offset, queryParams);
            return Client.Get<ContactHistory>(path, queryParams);
        }
    }
}