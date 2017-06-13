using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using System.IO;
using System;

namespace CallfireApiClient.Api.Campaigns
{
    public class CampaignSoundsApi
    {
        private const string SOUNDS_PATH = "/campaigns/sounds";
        private const string SOUNDS_ITEM_PATH = "/campaigns/sounds/{}";
        private const string SOUNDS_CALLS_PATH = "/campaigns/sounds/calls";
        private const string SOUNDS_FILES_PATH = "/campaigns/sounds/files";
        private const string SOUNDS_TTS_PATH = "/campaigns/sounds/tts";

        private readonly RestApiClient Client;

        internal CampaignSoundsApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find all campaign sounds that were created by the user.
        /// These are all of the available sounds to be used in campaigns.
        /// </summary>
        /// <param name="request">request object with different fields for search</param>
        /// <returns>page with campaign sound objects</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<CampaignSound> Find(FindSoundsRequest request)
        {
            return Client.Get<Page<CampaignSound>>(SOUNDS_PATH, request);
        }


        /// <summary>
        /// Returns a single CampaignSound instance for a given campaign sound id. This is the meta
        /// data to the sounds only.No audio data is returned from this API.
        /// </summary>
        /// <param name="id">id of campaign sound</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns>CampaignSound meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound Get(long id, string fields = null)
        {
            string path = SOUNDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<CampaignSound>(path, queryParams);
        }

        /// <summary>
        /// Delete a single CampaignSound instance for a given campaign sound id.
        /// </summary>
        /// <param name="id">id of campaign sound</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(long id)
        {
            string path = SOUNDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            Client.Delete(path);
        }

        /// <summary>
        /// Download the MP3 version of the hosted file.
        /// </summary>
        /// <param name="id">id of sound</param>
        /// <returns>mp3 file as input stream</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetMp3(long id)
        {
            string path = SOUNDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()) + ".mp3";
            return Client.GetFileData(path);
        }

        /// <summary>
        /// Download the WAV version of the hosted file.
        /// </summary>
        /// <param name="id">id id of sound</param>
        /// <returns>wav file as input stream</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetWav(long id)
        {
            string path = SOUNDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()) + ".wav";
            return Client.GetFileData(path);
        }

        /// <summary>
        /// Use this API to create a sound via phone call. Supply the required phone number in
        /// the CallCreateSound object inside of the request, and the user will receive a call
        /// shortly after with instructions on how to record a sound over the phone.
        /// </summary>
        /// <param name="callCreateSound"> request object to create campaign sound</param>
        /// <returns>ResourceId object with sound id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        [Obsolete]
        public ResourceId RecordViaPhone(CallCreateSound callCreateSound)
        {
            return Client.Post<ResourceId>(SOUNDS_CALLS_PATH, callCreateSound);
        }

        /// <summary>
        /// Use this API to create a sound via phone call. Supply the required phone number in
        /// the CallCreateSound object inside of the request, and the user will receive a call
        /// shortly after with instructions on how to record a sound over the phone.
        /// </summary>
        /// <param name="callCreateSound">request object to create campaign sound</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns>CampaignSound object with sound details</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound RecordViaPhoneAndGetSoundDetails(CallCreateSound callCreateSound, string fields)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Post<CampaignSound>(SOUNDS_CALLS_PATH, callCreateSound, queryParams);
        }

        /// <summary>
        /// Upload a MP3 or WAV file to account
        /// </summary>
        /// <param name="name">contact list name</param>
        /// <param name="pathToFile">path to MP3 or WAV file</param>
        /// <returns> ResourceId object with sound id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        [Obsolete]
        public ResourceId Upload(string pathToFile, string name = null)
        {
            return Client.PostFile<ResourceId>(SOUNDS_FILES_PATH, name, pathToFile);
        }

        /// <summary>
        /// Upload a MP3 or WAV file to account
        /// </summary>
        /// <param name="name">contact list name</param>
        /// <param name="pathToFile">path to MP3 or WAV file</param>
        /// <returns> CampaignSound object with sound details</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound UploadAndGetSoundDetails(string pathToFile, string name = null)
        {
            return UploadAndGetSoundDetails(pathToFile, name, null);
        }

        /// <summary>
        /// Upload a MP3 or WAV file to account
        /// </summary>
        /// <param name="name">file name</param>
        /// <param name="pathToFile">path to MP3 or WAV file</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns> CampaignSound object with sound details</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound UploadAndGetSoundDetails(string pathToFile, string name, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.PostFile<CampaignSound>(SOUNDS_FILES_PATH, name, pathToFile, queryParams);
        }

        /// <summary>
        /// Upload a MP3 or WAV file to account
        /// </summary>
        /// <param name="fileData">binary file data to upload</param>
        /// <param name="name">file name</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns> CampaignSound object with sound details</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound UploadAndGetSoundDetails(byte[] fileData, string name = null, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.PostFile<CampaignSound>(SOUNDS_FILES_PATH, fileData, name, null, queryParams);
        }

        /// <summary>
        /// Use this API to create a sound file via a supplied string of text.
        /// </summary>
        /// <param name="textToSpeech">TTS object to create</param>
        /// <returns> ResourceId object with sound id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        [Obsolete]
        public ResourceId CreateFromTts(TextToSpeech textToSpeech)
        {
            return Client.Post<ResourceId>(SOUNDS_TTS_PATH, textToSpeech);
        }

        /// <summary>
        /// Use this API to create a sound file via a supplied string of text.
        /// </summary>
        /// <param name="textToSpeech">TTS object to create</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns> CampaignSound object with sound details</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public CampaignSound CreateFromTtsAndGetSoundDetails(TextToSpeech textToSpeech, string fields)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Post<CampaignSound>(SOUNDS_TTS_PATH, textToSpeech, queryParams);
        }
    }
}