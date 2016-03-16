using System;
using RestSharp;
using CallfireApiClient.Api.Common.Model.Request;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Configuration;
using CallfireApiClient.Api.Common.Model;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System.Collections;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Net;

namespace CallfireApiClient
{
    /// <summary>
    /// REST client which makes HTTP calls to Callfire service
    /// </summary>
    public class RestApiClient
    {
        private const string DEFAULT_FILE_CONTENT_TYPE = "application/octet-stream";

        private readonly Logger Logger = new Logger();
        private readonly ISerializer JsonSerializer;
        private readonly IDeserializer JsonDeserializer;
        private static KeyValueConfigurationCollection ClientConfig;

        /// <summary>
        /// RestSharp client configured to query Callfire API
        /// <summary>/
        /// <returns>RestSharp client interface</returns>
        public IRestClient RestClient { get; set; }

        /// <summary>
        /// Returns base URL path for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of base URL path</returns>
        public string ApiBasePath { get; private set; }

        /// <summary>
        /// Returns HTTP request filters associated with API client
        /// </summary>
        /// <value>active filters.</value>
        public SortedSet<RequestFilter> Filters { get; }

        /// <summary>
        /// loads client configuration
        /// </summary>
        static RestApiClient() {
            ClientConfig = LoadAppSettings();
        }

        /// <summary>
        /// Get client configuration
        /// </summary>
        /// <value>configuration properties collection</value>
        public static KeyValueConfigurationCollection getClientConfig()
        {
            return ClientConfig;
        }

        /// <summary>
        /// REST API client constructor
        /// <summary>/
        /// <param name="authenticator">
        ///  authentication API authentication method
        /// </param>
        public RestApiClient(IAuthenticator authenticator)
        {
            ApiBasePath = ClientConfig[ClientConstants.CONFIG_API_BASE_PATH].Value;
            JsonSerializer = new CallfireJsonConverter();
            JsonDeserializer = JsonSerializer as IDeserializer;

            RestClient = new RestClient(ApiBasePath);
            RestClient.Authenticator = authenticator;
            RestClient.UserAgent = this.GetType().Assembly.GetName().Name + "-csharp-" + this.GetType().Assembly.GetName().Version;
            RestClient.AddHandler("application/json", JsonDeserializer);

            
            String proxyAddress = ClientConfig[ClientConstants.PROXY_ADDRESS_PROPERTY]?.Value; 
            String proxyCredentials = ClientConfig[ClientConstants.PROXY_CREDENTIALS_PROPERTY]?.Value; 

            if (!String.IsNullOrEmpty(proxyAddress))
            {
                Logger.Debug("Configuring proxy host for client: {} auth: {}", proxyAddress, proxyCredentials);
                char[] delimiterChars = { ':' };
                String[] parsedAddress = proxyAddress.Split(delimiterChars);
                String[] parsedCredentials = (proxyCredentials == null ? "" : proxyCredentials).Split(delimiterChars);
                int portValue = parsedAddress.Length > 1 ? ClientUtils.StrToIntDef(parsedAddress[1], ClientConstants.DEFAULT_PROXY_PORT) : ClientConstants.DEFAULT_PROXY_PORT;
                WebProxy proxy = new WebProxy(parsedAddress[0], portValue);
               
                if (!String.IsNullOrEmpty(proxyCredentials))
                {
                    if (parsedCredentials.Length > 1)
                    {
                        proxy.Credentials = new NetworkCredential(parsedCredentials[0], parsedCredentials[1]);
                    }
                    else
                    {
                        Logger.Debug("Proxy credentials have wrong format, must be username:password");
                    }
                }
                RestClient.Proxy = proxy;
            }

            Filters = new SortedSet<RequestFilter>();
        }

        /// <summary>
        /// Loads client's app settings config section
        /// </summary>
        public static KeyValueConfigurationCollection LoadAppSettings()
        {
            var path = typeof(RestApiClient).Assembly.Location;
            var config = ConfigurationManager.OpenExeConfiguration(path);
            var appSettings = (AppSettingsSection)config.GetSection("appSettings");
            var basePath = appSettings.Settings[ClientConstants.CONFIG_API_BASE_PATH];
            if (basePath == null || string.IsNullOrWhiteSpace(basePath.Value))
            {
                throw new CallfireClientException("Cannot read " + ClientConstants.CONFIG_API_BASE_PATH +
                    " property from configuration file at: " + path + ".config");
            }
           return appSettings.Settings;
        }

        /// <summary>
        /// Performs GET request to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="request">optional finder request with query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Get<T>(String path, FindRequest request = null) where T : new()
        {
            return Get<T>(path, ClientUtils.BuildQueryParams(request));
        }

        /// <summary>
        /// Performs GET request to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Get<T>(string path, IEnumerable<KeyValuePair<string, object>> queryParams) where T : new()
        {
            Logger.Debug("GET request to {0} with params: {1}", path, queryParams);
            var restRequest = CreateRestRequest(path, Method.GET, queryParams);
            restRequest.AddHeader("Accept", "*/*");
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs GET request to specified path
        /// <summary>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>byte array with file data</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Stream GetFileData(string path, IEnumerable<KeyValuePair<string, object>> queryParams = null)
        { 
            Logger.Debug("GET request to {0} with params: {1}", path, queryParams);
            var restRequest = CreateRestRequest(path, Method.GET, queryParams);

            restRequest.OnBeforeDeserialization = resp =>
            {
                string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (resp.Content.StartsWith(byteOrderMarkUtf8))
                    resp.Content = resp.Content.Remove(0, byteOrderMarkUtf8.Length);
            };

            restRequest.AddHeader("Accept", "*/*");
            return DoRequest(restRequest);
        }

        /// <summary>
        /// Performs POST request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">optional object to send</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Post<T>(String path, object payload = null) where T : new()
        {
            return Post<T>(path, payload, ClientUtils.EMPTY_MAP);
        }

        /// <summary>
        /// Performs POST request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">object to send</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Post<T>(String path, object payload, IEnumerable<KeyValuePair<string, object>> queryParams) where T : new()
        {
            var restRequest = CreateRestRequest(path, Method.POST, queryParams);
            if (payload != null)
            {
                validatePayload(payload);
                restRequest.AddJsonBody(payload);
                Logger.Debug("POST request to {0} params: {1} entity: \n{2}", path, queryParams, payload);
            }
            else
            {
                Logger.Debug("POST request to {0} params: {1}", path, queryParams);
            }
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs POST request with binary body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="fileName">name of file</param>
        /// <param name="filePath">path to file</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T PostFile<T>(String path, string fileName, string filePath, string contentType = null) where T : new()
        {
            var queryParams = ClientUtils.BuildQueryParams("name", fileName);
            var restRequest = CreateRestRequest(path, Method.POST, queryParams);
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFileBytes("file", File.ReadAllBytes(filePath), Path.GetFileName(filePath), contentType != null ? contentType : DEFAULT_FILE_CONTENT_TYPE);
            restRequest.AddParameter("name", fileName);
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs PUT request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">optional object to send</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Put<T>(String path, object payload = null) where T : new()
        {
            return Put<T>(path, payload, ClientUtils.EMPTY_MAP);
        }

        /// <summary>
        /// Performs PUT request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">object to send</param>
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Put<T>(String path, object payload, IEnumerable<KeyValuePair<string, object>> queryParams) where T : new()
        {
            var restRequest = CreateRestRequest(path, Method.PUT, queryParams);
            if (payload != null)
            {
                validatePayload(payload);
                restRequest.AddJsonBody(payload);
                Logger.Debug("PUT request to {0} params: {1} entity: \n{2}", path, queryParams, payload);
            }
            else
            {
                Logger.Debug("PUT request to {0} params: {1}", path, queryParams);
            }
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs DELETE request to specified path
        /// <summary>
        /// <param name="path">relative API request path</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(String path)
        {
            Delete(path, ClientUtils.EMPTY_MAP);
        }

        /// <summary>
        /// Performs DELETE request to specified path with query parameters
        /// <summary>
        /// <param name="path">relative API request path</param>
        /// <param name="queryParams">query parameters</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void Delete(String path, IEnumerable<KeyValuePair<string, object>> queryParams)
        {
            Logger.Debug("DELETE request to {0} with params: {1}", path, queryParams);
            var restRequest = CreateRestRequest(path, Method.DELETE, queryParams);
            DoRequest<object>(restRequest);
        }

        private T DoRequest<T>(IRestRequest request) where T : new()
        {
            FilterRequest(request);
            var response = RestClient.Execute<T>(request);
            if (response.Content == null)
            {
                Logger.Debug("received http code {0} with null entity, returning null", response.StatusCode);
                return default(T);
            }
            VerifyResponse(response);
            Logger.Debug("received entity: {0}", response.Content);

            return response.Data;
        }

        private Stream DoRequest(IRestRequest request)
        {
            FilterRequest(request);
            Stream downloadedStream = new MemoryStream();
            request.ResponseWriter = (ms) => ms.CopyTo(downloadedStream);
            var response = RestClient.Execute(request);
            if (response.Content == null)
            {
                Logger.Debug("received http code {0} with null file data, returning null", response.StatusCode);
                return null;
            }
            Logger.Debug("received file data: {0}", response.Content);
            VerifyResponse(response);

            return downloadedStream;
        }

        private void VerifyResponse(IRestResponse response)
        {
            int statusCode = (int)response.StatusCode;
            if (statusCode < 400 && response.ErrorException != null)
            {
                Logger.Error("request has failed: {0}", response.ErrorException);
                throw new CallfireClientException(response.ErrorMessage, response.ErrorException);
            }
            else if (statusCode >= 400)
            {
                ErrorMessage message;
                try
                {
                    message = JsonDeserializer.Deserialize<ErrorMessage>(response);
                }
                catch (Exception e)
                {
                    Logger.Error("deserialization of ErrorMessage has failed: {0}", e);
                    message = new ErrorMessage(statusCode, response.Content, ClientConstants.GENERIC_HELP_LINK);
                }
                switch (statusCode)
                {
                    case 400:
                        throw new BadRequestException(message);
                    case 401:
                        throw new UnauthorizedException(message);
                    case 403:
                        throw new AccessForbiddenException(message);
                    case 404:
                        throw new ResourceNotFoundException(message);
                    case 500:
                        throw new InternalServerErrorException(message);
                    default:
                        throw new CallfireApiException(message);
                }
            }
        }

        private IRestRequest CreateRestRequest(string path, Method method, IEnumerable<KeyValuePair<string, object>> queryParams = null)
        {
            var request = new RestRequest(path, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = JsonSerializer;
            if (queryParams != null)
            {
                foreach (KeyValuePair<string, object> pair in queryParams)
                {
                    var collection = pair.Value as ICollection;
                    if (collection != null)
                    {
                        foreach (var v in collection)
                        {
                            request.AddQueryParameter(pair.Key, v.ToString());
                        }
                    }
                    else if (pair.Value != null)
                    {
                        request.AddQueryParameter(pair.Key, pair.Value.ToString());
                    }
                }
            }
            return request;
        }

        private void FilterRequest(IRestRequest request)
        {
            foreach (RequestFilter filter in Filters)
            {
                filter.Filter(request);
            }
        }

        private void validatePayload(Object payload)
        {
            if (payload != null && payload is CallfireModel)
            {
                ((CallfireModel)payload).validate();
            }
        }
    }
}