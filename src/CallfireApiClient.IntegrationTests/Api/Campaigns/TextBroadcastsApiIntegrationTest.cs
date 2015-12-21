using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Campaigns
{
    [TestFixture, Ignore("temporary disabled")]
    public class TextBroadcastsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var broadcast = new TextBroadcast
            {   
                Name = "voice_broadcast",
                FromNumber = "12132212384",
                BigMessageStrategy = BigMessageStrategy.SEND_MULTIPLE,
                Message = "test_msg",
                LocalTimeRestriction = new LocalTimeRestriction
                {
                    BeginHour = 10,
                    BeginMinute = 10,
                    EndHour = 22,
                    EndMinute = 0,
                    Enabled = true
                },
                Recipients = new List<TextRecipient>
                {
                    new TextRecipient { PhoneNumber = "12132212384" },
                    new TextRecipient { PhoneNumber = "12132212385" }
                }
            };
            var id = Client.TextBroadcastsApi.Create(broadcast, true);
            var savedBroadcast = Client.TextBroadcastsApi.Get(id.Id);
            Assert.AreEqual(broadcast.Name, savedBroadcast.Name);

            savedBroadcast.Name = "updated_name";
            Client.TextBroadcastsApi.Update(savedBroadcast);

            var updatedBroadcast = Client.TextBroadcastsApi.Get(id.Id, "id,name");
            Assert.Null(updatedBroadcast.Status);
            Assert.NotNull(updatedBroadcast.Id);
            Assert.AreEqual(savedBroadcast.Name, updatedBroadcast.Name);
        }

        [Test]
        public void StartStopArchiveCampaign()
        {
            var campaign = Client.TextBroadcastsApi.Get(8729792003);
            Console.WriteLine(campaign);
            Assert.NotNull(campaign);
            // start
            Client.TextBroadcastsApi.Start((long)campaign.Id);
            campaign = Client.TextBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.RUNNING, campaign.Status);
            // stop
            Client.TextBroadcastsApi.Stop((long)campaign.Id);
            campaign = Client.TextBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.STOPPED, campaign.Status);
            // archive
            Client.TextBroadcastsApi.Archive((long)campaign.Id);
            campaign = Client.TextBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.ARCHIVED, campaign.Status);
        }

        [Test]
        public void GetBroadcastTexts()
        {
            var request = new GetByIdRequest { Id = 3 };
            var texts = Client.TextBroadcastsApi.GetTexts(request);
            Console.WriteLine(texts);
            Assert.That(texts.Items, Is.Not.Empty);
        }

        [Test]
        public void GetBroadcastStats()
        {
            var begin = DateTime.Now.AddDays(-5d);
            var end = DateTime.Now;
            var fields = "TotalOutboundCount,remainingOutboundCount";
            var stats = Client.TextBroadcastsApi.GetStats(3, fields, begin, end);
            Console.WriteLine(stats);
        }

        [Test]
        public void AddRecipientsAndAddRemoveBatches()
        {
            var findRequest = new FindBroadcastsRequest
            {
                Name = "updated_name",
                Limit = 1
            };
            var broadcasts = Client.TextBroadcastsApi.Find(findRequest);
            Console.WriteLine(broadcasts);
            Assert.That(broadcasts.Items, Is.Not.Empty);
            var id = broadcasts.Items[0].Id;

            // add recipients
            var recipients = new List<TextRecipient>
            {
                new TextRecipient { PhoneNumber = "12132212384" },
                new TextRecipient { PhoneNumber = "12132212385" }
            };
            var texts = Client.TextBroadcastsApi.AddRecipients((long)id, recipients);
            Console.WriteLine(texts);
            Assert.AreEqual(2, texts.Count);
            Assert.That(texts[0].Message, Is.StringStarting("test_msg"));

            // get batches
            var getBatchesRequest = new GetByIdRequest { Id = id };
            var batches = Client.TextBroadcastsApi.GetBatches(getBatchesRequest);
            Console.WriteLine(batches);

            // add batch
            var addBatchRequest = new AddBatchRequest
            {
                CampaignId = (long)id,
                Name = "new_batch",
                Recipients = new List<Recipient>
                {
                    new TextRecipient { PhoneNumber = "12132212384" },
                    new TextRecipient { PhoneNumber = "12132212385" }
                }
            };
            var resourceId = Client.TextBroadcastsApi.AddBatch(addBatchRequest);

            var updatedBatches = Client.TextBroadcastsApi.GetBatches(getBatchesRequest);
            Console.WriteLine(batches);
            Assert.AreEqual(batches.Items.Count + 1, updatedBatches.Items.Count);

            var getBatchRequest = new GetByIdRequest { Id = resourceId.Id };
            Batch savedBatch = Client.TextBroadcastsApi.GetBatch(getBatchRequest);
            Assert.True((bool)savedBatch.Enabled);
            Assert.AreEqual(addBatchRequest.Name, savedBatch.Name);

            savedBatch.Enabled = false;
            Client.TextBroadcastsApi.UpdateBatch(savedBatch);
            Batch updatedBatch = Client.TextBroadcastsApi.GetBatch(getBatchRequest);
            Assert.False((bool)updatedBatch.Enabled);
        }
    }
}

