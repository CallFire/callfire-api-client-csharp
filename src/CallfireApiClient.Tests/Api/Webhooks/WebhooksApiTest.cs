using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Collections.Generic;
using CallfireApiClient.Api.Webhooks.Model.Request;
using CallfireApiClient.Api.Webhooks.Model;

namespace CallfireApiClient.Tests.Api.Webhooks
{
    [TestFixture]
    public class WebhooksApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var responseJson = GetJsonPayload("/webhooks/webhooksApi/response/createWebhook.json");
            var requestJson = GetJsonPayload("/webhooks/webhooksApi/request/createWebhook.json");
            var restRequest = MockRestResponse(responseJson);

            var webhook = new Webhook
            {
                Name = "API hook",
                Resource = "textCampaign",
                Events = new HashSet<string>{ "start", "stop" },
                Callback = "http://yoursite.com/webhook"
            };
            var id = Client.WebhooksApi.Create(webhook);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/webhooks/webhooksApi/response/findWebhooks.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindWebhooksRequest
            {
                Limit = 5,
                Offset = 0,
                Enabled = false,
                Resource = "resource"
            };
            var webhooks = Client.WebhooksApi.Find(request);
            Assert.That(Serializer.Serialize(webhooks), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("resource") && p.Value.Equals("resource")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("enabled") && p.Value.Equals("False")));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/webhooks/webhooksApi/response/getWebhook.json");
            var restRequest = MockRestResponse(expectedJson);

            var webhook = Client.WebhooksApi.Get(11L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
            Assert.That(Serializer.Serialize(webhook), Is.EqualTo(expectedJson));

            Client.WebhooksApi.Get(11L, FIELDS);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void Update()
        {
            var expectedJson = GetJsonPayload("/webhooks/webhooksApi/request/updateWebhook.json");
            var restRequest = MockRestResponse(expectedJson);

            var webhook = new Webhook
            {
                Id = 11,
                Name = "API hook",
                Resource = "textCampaign",
                Events = new HashSet<string> { "stop" },
                Callback = "http://yoursite.com/webhook"
            };
            Client.WebhooksApi.Update(webhook);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, expectedJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();

            Client.WebhooksApi.Delete(11L);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }
    }
}