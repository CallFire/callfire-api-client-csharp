using NUnit.Framework;
using CallfireApiClient.Api.Webhooks.Model.Request;
using CallfireApiClient.Api.Webhooks.Model;
using System.Collections.Generic;

namespace CallfireApiClient.IntegrationTests.Api.Webhooks
{
    [TestFixture]
    public class WebhooksApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var api = Client.WebhooksApi;
            var webhook = new Webhook
            {
                Name = "test_name1",
                Callback = "test_callback",
                Resource = ResourceType.TEXT_BROADCAST,
                Events = new HashSet<ResourceEvent> { ResourceEvent.STARTED },
                SingleUse = true
            };
            var resourceId1 = api.Create(webhook);
            Assert.NotNull(resourceId1.Id);
            webhook.Name = "test_name2";
            var resourceId2 = api.Create(webhook);

            var findRequest = new FindWebhooksRequest
            {
                Limit = 30L,
                Name = "test_name1",
                Fields = "items(id,callback,name,resource,events,singleUse)"
            };
            var page = api.Find(findRequest);
            Assert.That(page.Items.Count > 0);
            Assert.AreEqual("test_name1", page.Items[0].Name);
            Assert.AreEqual("test_callback", page.Items[0].Callback);
            Assert.AreEqual(ResourceType.TEXT_BROADCAST, page.Items[0].Resource);
            Assert.AreEqual(1, page.Items[0].Events.Count);
            Assert.NotNull(page.Items[0].Id);
            Assert.True(page.Items[0].SingleUse.GetValueOrDefault());

            webhook = page.Items[0];
            webhook.Name = "test_name2";
            webhook.SingleUse = false;
            api.Update(webhook);
            Webhook updated = api.Get((long)webhook.Id);
            Assert.AreEqual(webhook.Resource, updated.Resource);
            Assert.False(page.Items[0].SingleUse.GetValueOrDefault());

            api.Delete((long)resourceId1.Id);
            api.Delete((long)resourceId2.Id);

            Assert.Throws<ResourceNotFoundException>(() => api.Get((long)resourceId1.Id));
            Assert.Throws<ResourceNotFoundException>(() => api.Get((long)resourceId2.Id));
        }

        [Test]
        public void TestResourceTypeOperations()
        {
            var api = Client.WebhooksApi;
            var resources = api.FindWebhookResources(null);
            Assert.NotNull(resources);
            Assert.AreEqual(resources.Count, 9);
            resources = api.FindWebhookResources("items(resource)");
            Assert.NotNull(resources[0].Resource);
            Assert.AreEqual(resources[0].SupportedEvents, null);

            WebhookResource resource = api.FindWebhookResource(ResourceType.CALL_BROADCAST, null);
            Assert.NotNull(resource);
            Assert.NotNull(resource.SupportedEvents);
            Assert.AreEqual(resource.Resource, "CallBroadcast");
            resource = api.FindWebhookResource(ResourceType.CALL_BROADCAST, "resource");
            Assert.NotNull(resource.Resource);
            Assert.AreEqual(resource.SupportedEvents, null);
        }
    }
}