using System;
using RestSharp;
using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Specialized;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using CallfireApiClient.Api.Common.Model;
using RestSharp.Deserializers;
using System.Text;


namespace CallfireApiClient
{
    /// <summary>
    /// REST client which makes HTTP calls to Callfire service
    /// </summary>
    public class RestApiClient
    {
        private readonly Logger Logger = new Logger();
        private readonly JsonDeserializer Deserializer;
        private readonly NameValueCollection EmptyParams = new NameValueCollection();

        /// <summary>
        /// RestSharp client configured to query Callfire API
        /// <summary>/
        /// <returns>RestSharp client interface</returns>
        public IRestClient RestClient { get; internal set; }

        /// <summary>
        /// Returns base URL path for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of base URL path</returns>
        public string ApiBasePath
        {
            get
            {
                return ConfigurationManager.AppSettings[ClientConstants.CONFIG_API_BASE_PATH];
            }
        }

        /// <summary>
        /// Returns HTTP request filters associated with API client
        /// </summary>
        /// <value>active filters.</value>
        public SortedSet<RequestFilter> Filters { get; }

        /// <summary>
        /// REST API client constructor
        /// <summary>/
        /// <param name="authenticator">
        ///  authentication API authentication method
        /// </param>
        public RestApiClient(IAuthenticator authenticator)
        {
            RestClient = new RestClient(ApiBasePath);
            RestClient.Authenticator = authenticator;
            RestClient.UserAgent = ConfigurationManager.AppSettings[ClientConstants.CONFIG_CLIENT_NAME];
            Deserializer = new JsonDeserializer();
            Filters = new SortedSet<RequestFilter>();
        }

        /// <summary>
        /// Performs GET request to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Get<T>(String path) where T: new()
        {
            return Get<T>(path, EmptyParams);
        }

        /// <summary>
        /// Performs GET request to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="request">finder request with query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Get<T>(String path, FindRequest request) where T: new()
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
        public T Get<T>(string path, NameValueCollection queryParams) where T: new()
        {
            Logger.Debug("GET request to {0} with params: {1}", path, queryParams);
            var restRequest = new RestRequest(path, Method.GET);
            AddQueryParams(restRequest, queryParams);
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs POST request to specified path with empty body
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Post<T>(String path) where T: new()
        {
            return Post<T>(path, null);
        }

        /// <summary>
        /// Performs POST request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">object to send</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Post<T>(String path, object payload) where T: new()
        {
            return Post<T>(path, payload, EmptyParams);
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
        public T Post<T>(String path, object payload, NameValueCollection queryParams) where T: new()
        {
            var restRequest = new RestRequest(path, Method.POST);
            AddQueryParams(restRequest, queryParams);
            if (payload != null)
            {
                restRequest.AddJsonBody(payload);
                Logger.Debug("POST request to {0} params: {1} entity \n{2}", path, queryParams, payload);
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
        /// <param name="queryParams">query parameters</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T PostFile<T>(String path, NameValueCollection queryParams) where T: new()
        {
            var restRequest = new RestRequest(path, Method.POST);
            restRequest.AddFile("file", queryParams["file"]);
            if (queryParams["name"] != null)
            {
                restRequest.AddParameter("name", queryParams["name"]);
            }

            Logger.Debug("POST file upload request to {0} with params {1}", path, queryParams);
            return DoRequest<T>(restRequest);
        }

        /// <summary>
        /// Performs PUT request with body to specified path
        /// <summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="path">relative API request path</param>
        /// <param name="payload">object to send</param>
        /// <returns>mapped object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public T Put<T>(String path, object payload) where T: new()
        {
            return Put<T>(path, payload, EmptyParams);
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
        public T Put<T>(String path, object payload, NameValueCollection queryParams) where T: new()
        {
            var restRequest = new RestRequest(path, Method.PUT);
            AddQueryParams(restRequest, queryParams);
            if (payload != null)
            {
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
            Delete(path, EmptyParams);
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
        public void Delete(String path, NameValueCollection queryParams)
        {
            Logger.Debug("DELETE request to {0} with params {1}", path, queryParams);
            var restRequest = new RestRequest(path, Method.DELETE);
            AddQueryParams(restRequest, queryParams);
            DoRequest<object>(restRequest);
        }

        private T DoRequest<T>(IRestRequest request) where T: new()
        {
            foreach (RequestFilter filter in Filters)
            {
                filter.Filter(request);
            }
            var response = RestClient.Execute<T>(request);
            if (response.Content == null)
            {
                Logger.Debug("received http code {0} with null entity, returning null", response.StatusCode);
                return default(T);
            }
            Logger.Debug("received entity: {0}", response.Content);
            VerifyResponse(response);

            return response.Data;
        }


        private void VerifyResponse(IRestResponse response)
        {
            if ((int)response.StatusCode >= 400)
            {
                var message = Deserializer.Deserialize<ErrorMessage>(response);
                switch ((int)response.StatusCode)
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

        private static void AddQueryParams(IRestRequest restRequest, NameValueCollection queryParams)
        {
            foreach (string key in queryParams.AllKeys)
            {
                restRequest.AddQueryParameter(key, queryParams[key]);
            }
        }
    }
}