using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using RestSharp;
using System.Linq;

namespace CallfireApiClient.Tests.Api.Campaigns
{
    [TestFixture]
    public class TextAutoRepliesApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var responseJson = GetJsonPayload("/campaigns/textAutoRepliesApi/response/createTextAutoReply.json");
            var requestJson = GetJsonPayload("/campaigns/textAutoRepliesApi/request/createTextAutoReply.json");
            var restRequest = MockRestResponse(responseJson);

            var textAutoReply = new TextAutoReply
            {
                Keyword = "CALLFIRE",
                Number = "67076",
                Message = "I am a leaf on the wind"
            };
            var id = Client.TextAutoRepliesApi.Create(textAutoReply);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/campaigns/textAutoRepliesApi/response/findTextAutoReplies.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindTextAutoRepliesRequest
            {
                Number = "1234567890",
                Limit = 5,
                Offset = 0
            };
            var autoReplies = Client.TextAutoRepliesApi.Find(request);
            Assert.That(Serializer.Serialize(autoReplies), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("number") && p.Value.Equals("1234567890")));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/campaigns/textAutoRepliesApi/response/getTextAutoReply.json");
            var restRequest = MockRestResponse(expectedJson);

            var autoReply = Client.TextAutoRepliesApi.Get(11L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
            Assert.That(Serializer.Serialize(autoReply), Is.EqualTo(expectedJson));

            Client.TextAutoRepliesApi.Get(11L, FIELDS);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();

            Client.TextAutoRepliesApi.Delete(11L);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }
    }
}

