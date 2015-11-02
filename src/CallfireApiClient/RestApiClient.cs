using System;
using RestSharp;


namespace CallfireApiClient
{
    /// <summary>
    /// REST client which makes HTTP calls to Callfire service
    /// </summary>
    public class RestApiClient
    {
        //    private static final Logger LOGGER = new Logger(RestApiClient.class);

        private RestClient restClient;
        //    private HttpClient httpClient;
        //    private JsonConverter jsonConverter;
        private Authentication authentication;
        private SortedSet<RequestFilter> filters = new TreeSet<>();

        /**
     * REST API client constructor. Currently available authentication methods: {@link BasicAuth}
     *
     * @param authentication API authentication method
     */
        public RestApiClient(Authentication authentication)
        {
            this.authentication = authentication;
            jsonConverter = new JsonConverter();
            httpClient = HttpClientBuilder.create()
            .setUserAgent(CallfireClient.getClientConfig().getProperty(USER_AGENT_PROPERTY))
            .build();
        }

        /**
     * Performs GET request to specified path
     *
     * @param path request path
     * @param type return entity type
     * @param <T>  return entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Get<T>(String path, TypeReference<T> type)
        {
            return Get(path, type, Enumerable.Empty<>());
        }

        /**
     * Performs GET request to specified path
     *
     * @param path    request path
     * @param type    return entity type
     * @param request finder request with query parameters
     * @param <T>     return entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Get<T>(String path, TypeReference<T> type, FindRequest request)
        {
            NameValueCollection queryParams = buildQueryParams(request);
            return get(path, type, queryParams);
        }

        /**
     * Performs GET request to specified path
     *
     * @param path        request path
     * @param type        return entity type
     * @param queryParams query parameters
     * @param <T>         return entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public  T Get<T>(String path, TypeReference<T> type, NameValueCollection queryParams)
        {
            try {
                String uri = getApiBasePath() + path;
                LOGGER.debug("GET request to {} with params: {}", uri, queryParams);
                RequestBuilder requestBuilder = RequestBuilder.get(uri)
                .addParameters(queryParams.toArray(new NameValuePair[queryParams.size()]));

                return doRequest(requestBuilder, type);
            } catch (IOException e) {
                throw new CallfireClientException(e);
            }
        }

        /**
     * Performs POST request to specified path with empty body
     *
     * @param path request path
     * @param type return entity type
     * @param <T>  return entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Post<T>(String path, TypeReference<T> type)
        {
            return Post(path, type, null);
        }

        /**
     * Performs POST request with binary body to specified path
     *
     * @param path   request path
     * @param type   response entity type
     * @param params request parameters
     * @param <T>    response entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T PostFile<T>(String path, TypeReference<T> type, Dictionary<string, object> queryParams)
        {
            try {
                String uri = getApiBasePath() + path;
                MultipartEntityBuilder entityBuilder = MultipartEntityBuilder.create();
                entityBuilder.setMode(HttpMultipartMode.BROWSER_COMPATIBLE);
                entityBuilder.addBinaryBody("file", (File)queryParams.get("file"));
                if (queryParams.get("name") != null) {
                    entityBuilder.addTextBody("name", (String)queryParams.get("name"));
                }
                RequestBuilder requestBuilder = RequestBuilder.post(uri).setEntity(entityBuilder.build());
                LOGGER.debug("POST file upload request to {} with params {}", uri, queryParams);

                return doRequest(requestBuilder, type);
            } catch (IOException e) {
                throw new CallfireClientException(e);
            }
        }

        /**
     * Performs POST request with body to specified path
     *
     * @param path    request path
     * @param type    response entity type
     * @param payload request payload
     * @param <T>     response entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Post<T>(String path, TypeReference<T> type, Object payload)
        {
            return post(path, type, payload, Enumerable.Empty<>());
        }

        /**
     * Performs POST request with body to specified path
     *
     * @param path        request path
     * @param type        response entity type
     * @param payload     request payload
     * @param queryParams query parameters
     * @param <T>         response entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Post<T>(String path, TypeReference<T> type, Object payload, List<NameValuePair> queryParams)
        {
            try {
                String uri = getApiBasePath() + path;
                RequestBuilder requestBuilder = RequestBuilder.post(uri)
                .setHeader(CONTENT_TYPE, APPLICATION_JSON.getMimeType())
                .addParameters(queryParams.toArray(new NameValuePair[queryParams.size()]));
                if (payload != null) {
                    String stringPayload = jsonConverter.serialize(payload);
                    requestBuilder.setEntity(EntityBuilder.create().setText(stringPayload).build());
                    logDebugPrettyJson("POST request to {} entity \n{}", uri, payload);
                } else {
                    LOGGER.debug("POST request to {}", uri);
                }

                return doRequest(requestBuilder, type);
            } catch (IOException e) {
                throw new CallfireClientException(e);
            }
        }

        /**
     * Performs PUT request with body to specified path
     *
     * @param path    request path
     * @param type    response entity type
     * @param payload request payload
     * @param <T>     response entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Put<T>(String path, TypeReference<T> type, Object payload)
        {
            return Put(path, type, payload, Enumerable.Empty<>());
        }

        /**
     * Performs PUT request with body to specified path
     *
     * @param path        request path
     * @param type        response entity type
     * @param payload     request payload
     * @param queryParams query parameters
     * @param <T>         response entity type
     * @return pojo mapped from json
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public T Put<T>(String path, TypeReference<T> type, Object payload, List<NameValuePair> queryParams)
        {
            try {
                String uri = getApiBasePath() + path;
                HttpEntity httpEntity = EntityBuilder.create().setText(jsonConverter.serialize(payload)).build();
                RequestBuilder requestBuilder = RequestBuilder.put(uri)
                .setHeader(CONTENT_TYPE, APPLICATION_JSON.getMimeType())
                .addParameters(queryParams.toArray(new NameValuePair[queryParams.size()]))
                .setEntity(httpEntity);
                logDebugPrettyJson("PUT request to {} entity \n{}", uri, payload);

                return doRequest(requestBuilder, type);
            } catch (IOException e) {
                throw new CallfireClientException(e);
            }
        }

        /**
     * Performs DELETE request to specified path
     *
     * @param path request path
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public void Delete(String path)
        {
            Delete(path, Enumerable.Empty<>());
        }

        /**
     * Performs DELETE request to specified path with query parameters
     *
     * @param path        request path
     * @param queryParams query parameters
     * @throws BadRequestException          in case HTTP response code is 400 - Bad request, the request was formatted improperly.
     * @throws UnauthorizedException        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.
     * @throws AccessForbiddenException     in case HTTP response code is 403 - Forbidden, insufficient permissions.
     * @throws ResourceNotFoundException    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.
     * @throws InternalServerErrorException in case HTTP response code is 500 - Internal Server Error.
     * @throws CallfireApiException         in case HTTP response code is something different from codes listed above.
     * @throws CallfireClientException      in case error has occurred in client.
     */
        public void delete(String path, List<NameValuePair> queryParams)
        {
            try {
                String uri = getApiBasePath() + path;
                LOGGER.debug("DELETE request to {} with params {}", uri, queryParams);
                RequestBuilder requestBuilder = RequestBuilder.delete(uri);
                requestBuilder.addParameters(queryParams.toArray(new NameValuePair[queryParams.size()]));
                doRequest(requestBuilder, STRING_TYPE);
                LOGGER.debug("delete executed");
            } catch (IOException e) {
                throw new CallfireClientException(e);
            }
        }

        /**
     * Returns base URL path for all Callfire's API 2.0 endpoints
     *
     * @return string representation of base URL path
     */
        public String getApiBasePath()
        {
            return CallfireClient.getClientConfig().getProperty(BASE_PATH_PROPERTY);
        }

        /**
     * Returns HTTP request filters associated with API client
     *
     * @return active filters
     */
        public SortedSet<RequestFilter> getFilters()
        {
            return filters;
        }

        private T DoRequest<T>(RequestBuilder requestBuilder, TypeReference<T> type)
        {
//        for (RequestFilter filter : filters) {
//            filter.filter(requestBuilder);
//        }
            HttpUriRequest httpRequest = authentication.apply(requestBuilder.build());
            HttpResponse response = httpClient.execute(httpRequest);

            int statusCode = response.getStatusLine().getStatusCode();
            HttpEntity httpEntity = response.getEntity();
            if (httpEntity == null) {
                LOGGER.debug("received http code {} with null entity, returning null", statusCode);
                return null;
            }
            String stringResponse = EntityUtils.toString(httpEntity, "UTF-8");
            verifyResponse(statusCode, stringResponse);

//        if (type.getType() == InputStream.class) {
//            return (T) httpEntity.getContent();
//        }

            T model = jsonConverter.deserialize(stringResponse, type);
            logDebugPrettyJson("received entity \n{}", model);
            return model;
        }

        private void verifyResponse(int statusCode, String stringResponse)
        {
            if (statusCode >= 400) {
                ErrorMessage message;
                try {
                    message = jsonConverter.deserialize(stringResponse, ERROR_MESSAGE_TYPE);
                } catch (CallfireClientException e) {
                    LOGGER.warn("cannot deserialize response entity.", e);
                    message = new ErrorMessage(statusCode, stringResponse, GENERIC_HELP_LINK);
                }
                switch (statusCode) {
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

        // makes extra deserialization to get pretty json string, enable only in case of debugging
        //    private void logDebugPrettyJson(String message, Object... params) throws JsonProcessingException {
        //        if (LOGGER.isDebugEnabled()) {
        //            for (int i = 0; i < params.length; i++) {
        //                if (params[i] instanceof CallfireModel) {
        //                    String prettyJson = jsonConverter.getMapper().writerWithDefaultPrettyPrinter()
        //                        .writeValueAsString(params[i]);
        //                    params[i] = prettyJson;
        //                }
        //            }
        //            LOGGER.debug(message, params);
        //        }
        //    }
    }
}