using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Generic;

namespace CallfireApiClient.Api.Contacts
{
    public class ContactListsApi
    {
        private const string LISTS_PATH = "/contacts/lists";
        private const string LISTS_ITEM_PATH = "/contacts/lists/{}";
        private const string LISTS_UPLOAD_PATH = "/contacts/lists/upload";
        private const string LISTS_ITEMS_PATH = "/contacts/lists/{}/items";
        private const string LISTS_ITEMS_CONTACT_PATH = "/contacts/lists/{}/items/{}";

        private readonly RestApiClient Client;

        internal ContactListsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find contact lists by id, name, number, etc...
        /// </summary>
        /// <param name="request">request object with fields to filter</param>
        /// <returns>paged list with contact lists</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<ContactList> Find(FindContactListsRequest request)
        { 
            return Client.Get<Page<ContactList>>(LISTS_PATH, request);
        }

        /// <summary>
        /// Creates a contact list for use with campaigns using 1 of 3 inputs. A List of Contact objects,
        /// a list of String E.164 numbers, or a list of CallFire contactIds can be used as the data source
        /// for the created contact list.After staging these contacts into the CallFire system, contact lists
        /// go through seven system safeguards that check the accuracy and consistency of the data. For example,
        /// our system checks if a number is formatted correctly, is invalid, is duplicated in another
        /// contact list, or is on a specific DNC list.The default resolution in these safeguards will be
        /// to remove contacts that are against these rules. If contacts are not being added to a list,
        /// this means the data needs to be properly formatted and correct before calling this API.
        /// </summary>
        /// <example>
        /// <code>
        /// var request = new CreateContactListRequest<Contact>
        /// {
        ///     Contacts = new List<Contact>
        ///     {
        ///         new Contact
        ///         {
        ///             FirstName = "Name1",
        ///             HomePhone = "16506190257"
        ///         },
        ///         new Contact
        ///         {
        ///             FirstName = "Name2",
        ///             HomePhone = "18778973473"
        ///         }
        ///     },
        ///     Name = "Name"
        /// };
        /// </code>
        /// </example>
        /// <param name="request">request object with provided contacts, list name and other values</param>
        /// <returns>newly created contact list id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create<T>(CreateContactListRequest<T> request)
        {
            return Client.Post<ResourceId>(LISTS_PATH, request);
        }

        /// <summary>
        /// Upload contact lists from CSV file
        /// Create contact list which includes list of contacts by file.
        /// </summary>
        /// <param name="name">contact list name</param>
        /// <param name="filePath">path to CSV file with contacts to upload</param>
        /// <returns>newly created contact list id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId CreateFromCsv(string name, string filePath)
        {
            return Client.PostFile<ResourceId>(LISTS_UPLOAD_PATH, name, filePath);
        }

        /// <summary>
        /// Get contact list by id
        /// </summary>
        /// <param name="id">id of contact list</param>
        /// <param name="fields">limit fields returned. Example fields=name,status</param>
        /// <returns>contact list object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ContactList Get(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = LISTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<ContactList>(path, queryParams);
        }

        /// <summary>
        /// Update contact list
        /// </summary>
        /// <param name="request">object contains properties to update</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Update(UpdateContactListRequest request)
        {
            Validate.NotBlank(request.Id.ToString(), "request.id cannot be null");
            string path = LISTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            Client.Put<object>(path, request);
        }

        /// <summary>
        /// Delete contact list
        /// </summary>
        /// <param name="id">contact list id</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(LISTS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }

        /// <summary>
        /// Find all entries in a given contact list. Property <b>request.id</b> required
        /// </summary>
        /// Property <b>request.id</b> required
        /// <param name="request">request object with properties to filter</param>
        /// <returns>paged list with contact objects from contact list</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Contact> GetListItems(GetByIdRequest request)
        {
            Validate.NotBlank(request.Id.ToString(), "request.id cannot be null");
            string path = LISTS_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.Id.ToString());
            return Client.Get<Page<Contact>>(path, request);
        }

        /// <summary>
        /// Add contact list items to list
        /// </summary>
        /// <param name="request">request object with contacts to add</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void AddListItems<T>(AddContactListContactsRequest<T> request)
        {
            Validate.NotBlank(request.ContactListId.ToString(), "request.contactListId cannot be null");
            string path = LISTS_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.ContactListId.ToString());
            Client.Post<object>(path, request);
        }

        /// <summary>
        /// Delete single contact list contact by id
        /// </summary>
        /// <param name="listId">id of contact list</param>
        /// <param name="contactId">id of item to remove</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void RemoveListItem(long listId, long contactId)
        {
            Validate.NotBlank(listId.ToString(), "listId cannot be blank");
            Validate.NotBlank(contactId.ToString(), "contactId cannot be blank");
            string path = LISTS_ITEMS_CONTACT_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, listId.ToString()).ReplaceFirst(ClientConstants.PLACEHOLDER, contactId.ToString());
            Client.Delete(path);
        }

        /// <summary>
        /// Delete contact list items
        /// </summary>
        /// <param name="contactListId">id of contact list</param>
        /// <param name="contactIds">ids of items to remove</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void RemoveListItems(long contactListId, List<long> contactIds)
        {
            Validate.NotBlank(contactListId.ToString(), "id cannot be blank");
            string path = LISTS_ITEMS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, contactListId.ToString());
            var queryParams = new List<KeyValuePair<string, object>>(1);
            ClientUtils.AddQueryParamIfSet("id", contactIds, queryParams);
            Client.Delete(path, queryParams);
        }
    }
}