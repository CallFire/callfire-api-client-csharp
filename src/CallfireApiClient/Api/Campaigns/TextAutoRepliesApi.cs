using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;

namespace CallfireApiClient.Api.Campaigns
{
    public class TextAutoRepliesApi
    {
        private const string TEXT_AUTO_REPLIES_PATH = "/texts/auto-replys";
        private const string TEXT_AUTO_REPLIES_ITEM_PATH = "/texts/auto-replys/{}";
        private readonly RestApiClient Client;

        internal TextAutoRepliesApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Query for text auto replies using optional number
        /// </summary>
        /// <param name="request">request object with filtering options</param>
        /// <returns>page with TextAutoReply objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<TextAutoReply> Find(FindTextAutoRepliesRequest request)
        {
            return Client.Get<Page<TextAutoReply>>(TEXT_AUTO_REPLIES_PATH, request);
        }

        ///
        /// Create and configure new text auto reply message for existing number.
        /// Auto-replies are text message replies sent to a customer when a customer replies to
        /// a text message from a campaign. A keyword will need to have been purchased before an Auto-Reply can be created.
        /// <param name="textAutoReply">auto-reply object to create</param>
        /// <returns>ResourceId object with id of created auto-reply</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Create(TextAutoReply textAutoReply)
        {
            return Client.Post<ResourceId>(TEXT_AUTO_REPLIES_PATH, textAutoReply);
        }

        ///
        /// Get text auto reply
        /// <param name="id">id of text auto reply object</param>
        /// <param name="fields">limit fields returned. Example fields=id,message</param>
        /// <returns>text auto reply object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public TextAutoReply Get(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            string path = TEXT_AUTO_REPLIES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.Get<TextAutoReply>(path, queryParams);
        }

        ///
        /// Delete text auto reply message attached to number.
        /// Can not delete a TextAutoReply currently active on a campaign.
        /// <param name="id">id of text auto reply</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            Client.Delete(TEXT_AUTO_REPLIES_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }
    }
}

