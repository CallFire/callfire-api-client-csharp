using System;
using NUnit.Framework;
using CallfireApiClient.Api.Account.Model;
using RestSharp;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Account.Model.Request;
using System.Linq;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Tests.Api.Account
{
    [TestFixture]
    public class MeApiTest : AbstractApiTest
    {
        [Test]
        public void GetAccount()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getAccount.json");
            MockRestResponse(expectedJson);
            UserAccount account = Client.MeApi.GetAccount();
            Assert.NotNull(account);
            Assert.That(Serializer.Serialize(account), Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetBillingPlanUsage()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getBillingPlanUsage.json");
            MockRestResponse(expectedJson);
            BillingPlanUsage billingPlanUsage = Client.MeApi.GetBillingPlanUsage();
            Assert.That(Serializer.Serialize(billingPlanUsage), Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetCreditsUsage()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getCreditsUsage.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new DateIntervalRequest
            {
                IntervalBegin = DateTime.UtcNow.AddMonths(-2),
                IntervalEnd = DateTime.UtcNow
            };


            CreditsUsage creditsUsage = Client.MeApi.GetCreditUsage(request);
            Assert.That(Serializer.Serialize(creditsUsage), Is.EqualTo(expectedJson));

            DateTime intBeg = (DateTime) request.IntervalBegin;
            DateTime intEnd = (DateTime) request.IntervalEnd;
            long ib = (long)(intBeg.ToUniversalTime() - ClientConstants.EPOCH).TotalMilliseconds;
            long ie = (long)(intEnd.ToUniversalTime() - ClientConstants.EPOCH).TotalMilliseconds;

            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("intervalBegin") && p.Value.Equals(ib.ToString())));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("intervalEnd") && p.Value.Equals(ie.ToString())));
        }


        [Test]
        public void GetCallerIds()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getCallerIds.json");
            MockRestResponse(expectedJson);
            IList<CallerId> callerIds = Client.MeApi.GetCallerIds();
            Assert.That(Serializer.Serialize(new ListHolder<CallerId>(callerIds)), Is.EqualTo(expectedJson));
        }

        [Test]
        public void SendVerificationCode()
        {
            var request = MockRestResponse();
            const string callerId = "1234567890";
        
            Client.MeApi.SendVerificationCode(callerId);
            Assert.AreEqual(Method.POST, request.Value.Method);
            Assert.That(request.Value.Resource, Is.StringContaining(callerId));
        }

        [Test]
        public void VerifyCallerId()
        {
            string requestJson = GetJsonPayload("/account/meApi/request/verifyCallerId.json");
            string responseJson = GetJsonPayload("/account/meApi/response/verifyCallerId.json");
            var restRequest = MockRestResponse(responseJson);
        
            var request = new CallerIdVerificationRequest
            {
                CallerId = "1234567890",
                VerificationCode = "1234"      
            };
            bool? verified = Client.MeApi.VerifyCallerId(request);
            Assert.That(Serializer.Serialize(verified), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);

            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Resource, Is.StringContaining(request.CallerId));
        }

        [Test]
        public void CreateApiCredentials()
        {
            string responseJson = GetJsonPayload("/account/meApi/response/createApiCredentials.json");
            string requestJson = GetJsonPayload("/account/meApi/request/createApiCredentials.json");
            var restRequest = MockRestResponse(responseJson);
        
            var credentials = new ApiCredentials
            {
                Name = "api_20_account"
            };
            var apiCredentials = Client.MeApi.CreateApiCredentials(credentials);
            Assert.That(Serializer.Serialize(apiCredentials), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void FindApiCredentials()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/findApiCredentials.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new CommonFindRequest
            {
                Limit = 1L,
                Offset = 5L
            };
            Page<ApiCredentials> apiCredentials = Client.MeApi.FindApiCredentials(request);
            Assert.That(Serializer.Serialize(apiCredentials), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));   
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));   
        }

        [Test]
        public void GetApiCredentials()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getApiCredentials.json");
            var restRequest = MockRestResponse(expectedJson);
        
            ApiCredentials apiCredentials = Client.MeApi.GetApiCredentials(11L, FIELDS);
            Assert.That(Serializer.Serialize(apiCredentials), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
        
            Client.MeApi.GetApiCredentials(11L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
        }

        [Test]
        public void DeleteApiCredentials()
        {
            var restRequest = MockRestResponse();

            Client.MeApi.DeleteApiCredentials(11L);
            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/11"));
        }
    }
}

