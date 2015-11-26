using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using RestSharp;
using System.Linq;

namespace CallfireApiClient.Tests.Api.CallsTexts
{
    [TestFixture]
    public class CallsApiTest : AbstractApiTest
    {
        [Test]
        public void SendCalls()
        {
            string requestJson = GetJsonPayload("/callstexts/callsApi/request/sendCalls.json");
            string responseJson = GetJsonPayload("/callstexts/callsApi/response/sendCalls.json");
            var restRequest = MockRestResponse(responseJson);

            CallRecipient r1 = new CallRecipient();
            r1.PhoneNumber = "12135551100";
            r1.liveMessage = "Why hello there!";
            CallRecipient r2 = new CallRecipient();
            r2.PhoneNumber = "12135551101";
            r2.liveMessage = "And hello to you too.";
            IList<Call> calls = Client.CallsApi.Send(new List<CallRecipient> { r1, r2 });

            Assert.That(Serializer.Serialize(new ListHolder<Call>(calls)), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));

            calls = Client.CallsApi.Send(new List<CallRecipient> { r1, r2 }, 100, FIELDS);
            Assert.That(restRequest.Value.Resource, !Is.StringContaining("fields" + FIELDS));
            Assert.That(restRequest.Value.Resource, !Is.StringContaining("campaignId=100"));
        }

        [Test]
        public void FindCalls()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/findCalls.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new FindCallsRequest();
            request.States = new List<Call.StateType> { Call.StateType.CALLBACK, Call.StateType.DISABLED };
            request.Id = new List<long> { 1, 2, 3 };
            request.Limit = 5;
            request.Offset = 0;

            Page<Call> calls = Client.CallsApi.Find(request);

            Assert.That(Serializer.Serialize(calls), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("2")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("3")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("CALLBACK")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("DISABLED")));
        }

        [Test]
        public void GetCall()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCall.json");
            var restRequest = MockRestResponse(expectedJson);

            Call call = Client.CallsApi.Get(1); 
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));

            call = Client.CallsApi.Get(1, FIELDS);
            Assert.That(Serializer.Serialize(call), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}

