using System;
using CallfireApiClient.Api.Account.Model;
using System.Collections.Specialized;
using System.Collections.Generic;
using CallfireApiClient.Api.Account.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Account
{
    public class MeApi
    {
        private const string ME_ACCOUNT_PATH = "/me/account";
        private const string ME_BILLING_PATH = "/me/billing/plan-usage";
        private const string ME_API_CREDS_PATH = "/me/api/credentials";
        private const string ME_API_CREDS_ITEM_PATH = "/me/api/credentials/{}";
        private const string ME_CALLERIDS_PATH = "/me/callerids";
        private const string ME_CALLERIDS_CODE_PATH = "/me/callerids/{}";
        private const string ME_CALLERIDS_VERIFY_PATH = "/me/callerids/{}/verification-code";
        private readonly RestApiClient Client;

        internal MeApi(RestApiClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Find account details for the user. Details include name, email, and basic account permissions.
        /// GET /me/account
        /// </summary>
        /// <returns>user's account</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public UserAccount GetAccount()
        {
            return Client.Get<UserAccount>(ME_ACCOUNT_PATH);
        }

        /// <summary>
        /// Get Plan usage statistics
        /// </summary>
        /// <returns>BillingPlanUsage object</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public BillingPlanUsage GetBillingPlanUsage()
        {
            return Client.Get<BillingPlanUsage>(ME_BILLING_PATH);
        }

        /// <summary>
        /// Returns a list of verified caller ids. If the number is not shown in the list,
        /// then it is not verified, and will have to send for a verification code.
        /// </summary>
        /// <returns>list of callerId numbers</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public IList<CallerId> GetCallerIds()
        {
            return Client.Get<ListHolder<CallerId>>(ME_CALLERIDS_PATH).Items;
        }

        /// <summary>
        /// Send generated verification code to callerid number.
        /// The verification code is delivered via a phone call.
        /// After receiving verification code on phone call POST /callerids/{callerid}/verification-code to verify number.
        /// </summary>
        /// <param name="callerid">callerid number</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void SendVerificationCode(String callerid)
        {
            Validate.NotBlank(callerid, "callerid cannot be blank");
            Client.Post<object>(ME_CALLERIDS_CODE_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, callerid));
        }

        /// <summary>
        /// Verify callerId by providing calling number and verificationCode received on phone.
        /// </summary>
        /// <param name="request">request object</param>
        /// <returns>true or false depending on whether verification was successful or not.</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public bool? VerifyCallerId(CallerIdVerificationRequest request)
        {
            Validate.NotBlank(request.CallerId, "callerid cannot be blank");
            string path = ME_CALLERIDS_VERIFY_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, request.CallerId);
            return Convert.ToBoolean(Client.Post<object>(path, request));
        }

        /// <summary>
        /// Create API credentials for the CallFire API. This endpoint requires full CallFire account
        /// credentials to be used, authenticated using Basic Authentication. At this time, the user
        /// can only supply the name for the credentials. The generated credentials can be used to
        /// access any endpoint on the CallFire API. ApiCredentials.name property required
        /// </summary>
        /// <param name="credentials">API credentials to create</param>
        /// <returns>created credentials</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ApiCredentials CreateApiCredentials(ApiCredentials credentials)
        {
            return Client.Post<ApiCredentials>(ME_API_CREDS_PATH, credentials);
        }

        /// <summary>
        /// Find API credentials associated with current account
        /// </summary>
        /// <param name="request">request with properties to filter</param>
        /// <returns>paged credentials list</returns>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public Page<ApiCredentials> FindApiCredentials(CommonFindRequest request)
        {
            return Client.Get<Page<ApiCredentials>>(ME_API_CREDS_PATH, request);
        }

        /// <summary>
        /// Get API credentials by id
        /// </summary>
        /// <param name="id">id of credentials</param>
        /// <param name="fields">limit fields returned. Example fields=id,name</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public ApiCredentials GetApiCredentials(long id, string fields = null)
        {
            var queryParams = ClientUtils.BuildQueryParams("fields", fields);
            return Client.Get<ApiCredentials>(ME_API_CREDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER,
                    id.ToString()), queryParams);
        }

        /// <summary>
        /// Delete API credentials by id
        /// </summary>
        /// <param name="id">id of credentials</param>
        /// <exception cref="BadRequestException">          in case HTTP response code is 400 - Bad request, the request was formatted improperly.</exception>
        /// <exception cref="UnauthorizedException">        in case HTTP response code is 401 - Unauthorized, API Key missing or invalid.</exception>
        /// <exception cref="AccessForbiddenException">     in case HTTP response code is 403 - Forbidden, insufficient permissions.</exception>
        /// <exception cref="ResourceNotFoundException">    in case HTTP response code is 404 - NOT FOUND, the resource requested does not exist.</exception>
        /// <exception cref="InternalServerErrorException"> in case HTTP response code is 500 - Internal Server Error.</exception>
        /// <exception cref="CallfireApiException">         in case HTTP response code is something different from codes listed above.</exception>
        /// <exception cref="CallfireClientException">      in case error has occurred in client.</exception>
        public void DeleteApiCredentials(long id)
        {
            Client.Delete(ME_API_CREDS_ITEM_PATH.ReplaceFirst(ClientConstants.PLACEHOLDER, id.ToString()));
        }
    }
}

