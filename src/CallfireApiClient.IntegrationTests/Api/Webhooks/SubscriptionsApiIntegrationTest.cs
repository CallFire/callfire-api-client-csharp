using System;
using NUnit.Framework;
using CallfireApiClient.Api.Webhooks.Model;
using CallfireApiClient.Api.Webhooks.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Webhooks
{
    [TestFixture, Ignore("temporary disabled")]
    public class SubscriptionsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var subscription = new Subscription
            {
                TriggerEvent = TriggerEvent.CAMPAIGN_FINISHED,
                NotificationFormat = NotificationFormat.JSON,
                Endpoint = "http://your-site.com/endpoint"
            };
            var resourceId1 = Client.SubscriptionsApi.Create(subscription);
            Assert.NotNull(resourceId1.Id);
            subscription.NotificationFormat = NotificationFormat.SOAP;
            var resourceId2 = Client.SubscriptionsApi.Create(subscription);

            var findRequest = new FindSubscriptionsRequest
            {
                Limit = 30,
                Format = NotificationFormat.SOAP,
                Fields = "items(id,notificationFormat)"
            };
            var page = Client.SubscriptionsApi.Find(findRequest);
            Assert.That(page.Items, Has.Count.GreaterThan(0));

            subscription = page.Items[0];
            Assert.Null(subscription.Endpoint);
            Assert.Null(page.Items[0].TriggerEvent);
            Assert.NotNull(subscription.Id);
            Assert.AreEqual(NotificationFormat.SOAP, page.Items[0].NotificationFormat);

            subscription.Endpoint = subscription.Endpoint + "1";
            Client.SubscriptionsApi.Update(subscription);
            var updated = Client.SubscriptionsApi.Get((long)subscription.Id);
            Assert.AreEqual(subscription.Endpoint, updated.Endpoint);

            Client.SubscriptionsApi.Delete((long)resourceId1.Id);
            Client.SubscriptionsApi.Delete((long)resourceId2.Id);

            Assert.Throws<ResourceNotFoundException>(() => Client.SubscriptionsApi.Get((long)resourceId1.Id));
            Assert.Throws<ResourceNotFoundException>(() => Client.SubscriptionsApi.Get((long)resourceId2.Id));
        }
    }
}

