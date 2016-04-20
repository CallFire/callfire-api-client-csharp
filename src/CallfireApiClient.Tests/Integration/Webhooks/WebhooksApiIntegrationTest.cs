using NUnit.Framework;
using CallfireApiClient.Api.Webhooks.Model.Request;
using CallfireApiClient.Api.Webhooks.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Tests.Integration.Webhooks
{
    [TestFixture, Ignore("temporary disabled")]
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
                Events = new HashSet<ResourceEvent> { ResourceEvent.STARTED }
            };
            var resourceId1 = api.Create(webhook);
            Assert.NotNull(resourceId1.Id);
            webhook.Name = "test_name2";
            var resourceId2 = api.Create(webhook);

            var findRequest = new FindWebhooksRequest
            {
                Limit = 30L,
                Name = "test_name1",
                Fields = "items(id,callback,name,resource,events)"
            };
            var page = api.Find(findRequest);
            Assert.That(page.Items, Has.Count.EqualTo(1));
            Assert.AreEqual("test_name1", page.Items[0].Name);
            Assert.AreEqual("test_callback", page.Items[0].Callback);
            Assert.AreEqual(ResourceType.TEXT_BROADCAST, page.Items[0].Resource);
            Assert.AreEqual(1, page.Items[0].Events.Count);
            Assert.NotNull(page.Items[0].Id);

            webhook = page.Items[0];
            webhook.Resource = ResourceType.VOICE_BROADCAST;
            api.Update(webhook);
            Webhook updated = api.Get((long)webhook.Id);
            Assert.AreEqual(webhook.Resource, updated.Resource);

            api.Delete((long)resourceId1.Id);
            api.Delete((long)resourceId2.Id);

            Assert.Throws<ResourceNotFoundException>(() => api.Get((long)resourceId1.Id));
            Assert.Throws<ResourceNotFoundException>(() => api.Get((long)resourceId2.Id));
        }
    }
}

