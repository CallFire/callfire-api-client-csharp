using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Campaigns.Model;
using System.IO;

namespace CallfireApiClient.Api.CallsTexts
{
    public class CallsApi
    {
        private const string CALLS_PATH = "/calls";
        private const string CALLS_ITEM_PATH = "/calls/{}";
        private static string CALLS_ITEM_RECORDINGS_PATH = "/calls/{}/recordings";
        private static string CALLS_ITEM_RECORDING_BY_NAME_PATH = "/calls/{}/recordings/{}";
        private static string CALLS_ITEM_MP3_RECORDING_BY_NAME_PATH = "/calls/{}/recordings/{}.mp3";
        private static string CALLS_ITEM_RECORDING_BY_ID_PATH = "/calls/recordings/{}";
        private static string CALLS_ITEM_MP3_RECORDING_BY_ID_PATH = "/calls/recordings/{}.mp3";

        private readonly RestApiClient Client;

        internal CallsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Finds all calls sent or received by the user, filtered by different properties, broadcast id,
        /// toNumber, fromNumber, label, state, etc.Use "campaignId=0" parameter to query
        /// for all calls sent through the POST /calls API {@link CallsApi#send(List)}.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>paged list with call objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<Call> Find(FindCallsRequest request)
        {
            return Client.Get<Page<Call>>(CALLS_PATH, request);
        }

        /// <summary>
        /// Get call by id
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>call object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Call Get(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = CALLS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<Call>(path, queryParams);
        }

        /// <summary>
        /// Send calls to recipients through default campaign.
        /// Use the API to quickly send individual calls.
        /// A verified Caller ID and sufficient credits are required to make a call.
        /// </summary>
        /// <param name="recipients">call recipients</param>
        /// <param name="campaignId">specify a campaignId to send calls quickly on a previously created campaign</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>list with created call objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Call> Send(IList<CallRecipient> recipients, long? campaignId = null, string fields = null)
        {
            Validate.NotBlank(recipients.ToString(), "recipients cannot be blank");
            var queryParams = new List<KeyValuePair<string, object>>(2);
            ClientUtils.AddQueryParamIfSet("campaignId", campaignId, queryParams);
            ClientUtils.AddQueryParamIfSet("fields", fields, queryParams);      
            return Client.Post<ListHolder<Call>>(CALLS_PATH, recipients, queryParams).Items;
        }

        /// <summary>
        /// Send calls to recipients through default campaign.
        /// Use the API to quickly send individual calls.
        /// A verified Caller ID and sufficient credits are required to make a call.
        /// </summary>
        /// <param name="request">request object with different fields to filter</param>
        /// <returns>list with created call objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<Call> Send(SendCallsRequest request)
        {
            Validate.NotBlank(request.Recipients.ToString(), "recipients cannot be blank");
            var queryParams = ClientUtils.BuildQueryParams(request);
            return Client.Post<ListHolder<Call>>(CALLS_PATH, request.Recipients, queryParams).Items;
        }

        /// <summary>
        /// Returns call recordings for a call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns>CallRecording objects list</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<CallRecording> GetCallRecordings(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = CALLS_ITEM_RECORDINGS_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<ListHolder<CallRecording>>(path, queryParams).Items;
        }

        /// <summary>
        /// Returns call recording by name
        /// </summary>
        /// <param name="callId">id of call</param>
        /// <param name="recordingName">name of call recording</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>CallRecording object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CallRecording GetCallRecordingByName(long callId, string recordingName, string fields = null)
        {
            Validate.NotBlank(callId.ToString(), "callId cannot be blank");
            Validate.NotBlank(recordingName, "recordingName cannot be blank");
            string path = CALLS_ITEM_RECORDING_BY_NAME_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, callId.ToString()).ReplaceFirst(ClientConstants.PLACEHOLDER, recordingName);
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<CallRecording>(path, queryParams);
        }

        /// <summary>
        /// Download call mp3 recording by name
        /// </summary>
        /// <param name="callId">id of call</param>
        /// <param name="recordingName">name of call recording</param>
        /// <returns>Call recording meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetCallRecordingMp3ByName(long callId, string recordingName)
        {
            Validate.NotBlank(callId.ToString(), "callId cannot be blank");
            Validate.NotBlank(recordingName, "recordingName cannot be blank");
            string path = CALLS_ITEM_MP3_RECORDING_BY_NAME_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, callId.ToString()).ReplaceFirst(ClientConstants.PLACEHOLDER, recordingName);
            return Client.GetFileData(path);
        }

        /// <summary>
        /// Returns call recording by id
        /// </summary>
        /// <param name="id">id of call recording</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <returns>CallRecording objects list</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CallRecording GetCallRecording(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = CALLS_ITEM_RECORDING_BY_ID_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<CallRecording>(path, queryParams);
        }

        /// <summary>
        /// Download call mp3 recording by id
        /// </summary>
        /// <param name="id">id of call</param>
        /// <returns>Call recording meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetCallRecordingMp3(long id)
        {
            Validate.NotBlank(id.ToString(), "callId cannot be blank");
            string path = CALLS_ITEM_MP3_RECORDING_BY_ID_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            return Client.GetFileData(path);
        }
    }
}
