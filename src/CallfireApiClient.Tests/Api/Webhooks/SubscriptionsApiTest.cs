using System;
using NUnit.Framework;
using CallfireApiClient.Api.Webhooks.Model;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Webhooks.Model.Request;

namespace CallfireApiClient.Tests.Api.Webhooks
{
    [TestFixture]
    public class SubscriptionsApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var responseJson = GetJsonPayload("/webhooks/subscriptionsApi/response/createSubscription.json");
            var requestJson = GetJsonPayload("/webhooks/subscriptionsApi/request/createSubscription.json");
            var restRequest = MockRestResponse(responseJson);

            var subscription = new Subscription
            {
                Enabled = true,
                Endpoint = "http://www.example.com/endpoint",
                NotificationFormat = NotificationFormat.JSON,
                BroadcastId = 14L,
                TriggerEvent = TriggerEvent.CAMPAIGN_STARTED
            };
            var id = Client.SubscriptionsApi.Create(subscription);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/webhooks/subscriptionsApi/response/findSubscriptions.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindSubscriptionsRequest
            {
                Limit = 5,
                Offset = 0,
                CampaignId = 111,
                Format = NotificationFormat.JSON
            };
            var webhooks = Client.SubscriptionsApi.Find(request);
            Assert.That(Serializer.Serialize(webhooks), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("campaignId") && p.Value.Equals("111")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("format") && p.Value.Equals("JSON")));

        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/webhooks/subscriptionsApi/response/getSubscription.json");
            var restRequest = MockRestResponse(expectedJson);

            var webhook = Client.SubscriptionsApi.Get(11L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
            Assert.That(Serializer.Serialize(webhook), Is.EqualTo(expectedJson));

            Client.SubscriptionsApi.Get(11L, FIELDS);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));

        }

        [Test]
        public void Update()
        {
            var expectedJson = GetJsonPayload("/webhooks/subscriptionsApi/request/updateSubscription.json");
            var restRequest = MockRestResponse(expectedJson);

            var subscription = new Subscription
            {
                Id = 11L,
                BroadcastId = 15L,
                TriggerEvent = TriggerEvent.CAMPAIGN_FINISHED
            };
            Client.SubscriptionsApi.Update(subscription);

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

