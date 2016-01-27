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
    public class TextsApiTest : AbstractApiTest
    {
        [Test]
        public void SendTexts()
        {
            string requestJson = GetJsonPayload("/callstexts/textsApi/request/sendTexts.json");
            string responseJson = GetJsonPayload("/callstexts/textsApi/response/sendTexts.json");
            var restRequest = MockRestResponse(responseJson);

            TextRecipient r1 = new TextRecipient { PhoneNumber = "12135551100", Message = "Hello World!" };
            TextRecipient r2 = new TextRecipient { PhoneNumber = "12135551101", Message = "Testing 1 2 3" };

            IList<CallfireApiClient.Api.CallsTexts.Model.Text> texts = Client.TextsApi.Send(new List<TextRecipient> { r1, r2 });

            Assert.That(Serializer.Serialize(new ListHolder<CallfireApiClient.Api.CallsTexts.Model.Text>(texts)), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));

            texts = Client.TextsApi.Send(new List<TextRecipient> { r1, r2 }, 100, FIELDS);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("campaignId") && p.Value.Equals("100")));
        }

        [Test]
        public void FindTexts()
        {
            string expectedJson = GetJsonPayload("/callstexts/textsApi/response/findTexts.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new FindTextsRequest
            {
                Limit = 5,
                Offset = 0,
                States = new List<StateType> { StateType.CALLBACK, StateType.DISABLED },
                Id = new List<long> { 1, 2, 3 },
                BatchId = 1001
            };

            Page<CallfireApiClient.Api.CallsTexts.Model.Text> texts = Client.TextsApi.Find(request);

            Assert.That(Serializer.Serialize(texts), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("batchId") && p.Value.Equals("1001")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("2")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("3")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("CALLBACK")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("DISABLED")));
        }

        [Test]
        public void GetText()
        {
            string expectedJson = GetJsonPayload("/callstexts/textsApi/response/getText.json");
            var restRequest = MockRestResponse(expectedJson);

            CallfireApiClient.Api.CallsTexts.Model.Text text = Client.TextsApi.Get(1);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));

            text = Client.TextsApi.Get(1, FIELDS);
            Assert.That(Serializer.Serialize(text), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}

