using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using System.IO;
using CallfireApiClient.Api.Common.Settings;

namespace CallfireApiClient.Api.CallsTexts
{
    public class MediaApi
    {
        private const string MEDIA_PATH = "/media";
        private const string MEDIA_FILE_PATH = "/media/{}/file";
        private const string MEDIA_ITEM_PATH = "/media/{}";
        private const string MEDIA_ITEM_ID_PATH = "/media/{}.{}";
        private const string MEDIA_ITEM_KEY_PATH = "/media/public/{}.{}";

        private readonly RestApiClient Client;

        internal MediaApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Upload media file to account
        /// </summary>
        /// <param name="file">file to upload</param>
        /// <param name="name">name for file uploaded</param>
        /// <returns>ResourceId object with sound id</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ResourceId Upload(string pathToFile, string name = null)
        {
            MediaType type = EnumHelper.EnumFromDescription<MediaType>((Path.GetExtension(pathToFile).Replace(".", "")));
            return Client.PostFile<ResourceId>(MEDIA_PATH, name, pathToFile, EnumHelper.EnumMemberAttr<MediaType>(type));
        }

        /// <summary>
        /// Returns a single Media instance for a given media file id. This is the metadata
        /// for the media only.No content data is returned from this API.
        /// </summary>
        /// <param name="id">id of media file</param>
        /// <param name="fields">Limit text fields returned. Example fields=limit,offset,items(id,message)</param>
        /// <returns>Media meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Media Get(long id, string fields = null)
        {
            Validate.NotBlank(id.ToString(), "id cannot be blank");
            string path = MEDIA_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString());
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);

            return Client.Get<Media>(path, queryParams);
        }

        /// <summary>
        /// Returns media file's data as stream, in case there is no appropriate MediaType for your media file pass MediaType.UNKNOWN
        /// </summary>
        /// <param name="id">id of media file</param>
        /// <param name="type">media type: jpeg, png, gif, mp3, mp4, wav</param>
        /// <returns>Media meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetData(long id, MediaType type)
        {
            string path = type == MediaType.UNKNOWN ? MEDIA_FILE_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()) : 
                MEDIA_ITEM_ID_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()).ReplaceFirst(ClientConstants.PLACEHOLDER, EnumHelper.DescriptionAttr(type));

            return Client.GetFileData(path);
        }

        /// <summary>
        /// Returns media file's data as stream
        /// </summary>
        /// <param name="key">key of media file</param>
        /// <param name="type">media type: jpeg, png, gif, mp3, mp4, wav</param>
        /// <returns>Media meta object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetData(string key, MediaType type)
        {
            Validate.NotBlank(key, "key cannot be blank");
            string path = MEDIA_ITEM_KEY_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, key).ReplaceFirst(ClientConstants.PLACEHOLDER, type.ToString());

            return Client.GetFileData(path);
        }
    }
}