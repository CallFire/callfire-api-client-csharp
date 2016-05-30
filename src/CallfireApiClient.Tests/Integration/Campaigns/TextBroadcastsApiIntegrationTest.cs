using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Tests.Integration.Campaigns
{
    [TestFixture, Ignore("temporary disabled")]
    public class TextBroadcastsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var broadcast = new TextBroadcast
            {
                Name = "text_broadcast",
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
                    new TextRecipient { PhoneNumber = "14246525473" },
                    new TextRecipient { PhoneNumber = "12132041238" }
                },
                ResumeNextDay = true
            };
            var id = Client.TextBroadcastsApi.Create(broadcast, true);
            var savedBroadcast = Client.TextBroadcastsApi.Get(id.Id);
            Assert.AreEqual(broadcast.Name, savedBroadcast.Name);
            Assert.AreEqual(savedBroadcast.ResumeNextDay, true);
            savedBroadcast.Name = "updated_name";
            savedBroadcast.ResumeNextDay = false;
            Client.TextBroadcastsApi.Update(savedBroadcast);

            var updatedBroadcast = Client.TextBroadcastsApi.Get(id.Id, "id,name");
            Assert.Null(updatedBroadcast.Status);
            Assert.NotNull(updatedBroadcast.Id);
            Assert.AreEqual(savedBroadcast.Name, updatedBroadcast.Name);
            Assert.AreEqual(savedBroadcast.ResumeNextDay, false);
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
            var broadcast = new TextBroadcast
            {
                Name = "text_broadcast_1",
                Message = "test_msg",
                Recipients = new List<TextRecipient>
                {
                    new TextRecipient { PhoneNumber = "14246525473" }
                }
            };
            var broadcastId = Client.TextBroadcastsApi.Create(broadcast, false);

            var request = new GetByIdRequest { Id = broadcastId.Id };
            var texts = Client.TextBroadcastsApi.GetTexts(request);
            Console.WriteLine(texts);
            Assert.That(texts.Items, Is.Not.Empty);

            long testBatchId = (long)texts.Items[0].BatchId;

            request = new GetBroadcastCallsTextsRequest { Id = broadcastId.Id, batchId = testBatchId };
            texts = Client.TextBroadcastsApi.GetTexts(request);
            Assert.AreEqual(texts.Items[0].BatchId, testBatchId);
        }

        [Test]
        public void GetBroadcastStats()
        {
            var broadcast = new TextBroadcast
            {
                Name = "text_broadcast_2",
                Message = "test_msg",
                Recipients = new List<TextRecipient>
                {
                    new TextRecipient { PhoneNumber = "12132041238" }
                }
            };
            var broadcastId = Client.TextBroadcastsApi.Create(broadcast, true);

            var begin = DateTime.Now.AddDays(-5d);
            var end = DateTime.Now;
            var fields = "TotalOutboundCount,remainingOutboundCount";
            var stats = Client.TextBroadcastsApi.GetStats(broadcastId.Id, fields, begin, end);
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
                new TextRecipient { PhoneNumber = "14246525473" },
                new TextRecipient { PhoneNumber = "12132041238" }
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
                    new TextRecipient { PhoneNumber = "14246525473" },
                    new TextRecipient { PhoneNumber = "12132041238" }
                }
            };
            var resourceId = Client.TextBroadcastsApi.AddBatch(addBatchRequest);

            var updatedBatches = Client.TextBroadcastsApi.GetBatches(getBatchesRequest);
            Console.WriteLine(batches);
            Assert.AreEqual(batches.Items.Count + 1, updatedBatches.Items.Count);

            Batch savedBatch = Client.BatchesApi.Get(resourceId.Id);
            Assert.True((bool)savedBatch.Enabled);
            Assert.AreEqual(addBatchRequest.Name, savedBatch.Name);

            savedBatch.Enabled = false;
            Client.BatchesApi.Update(savedBatch);
            Batch updatedBatch = Client.BatchesApi.Get(resourceId.Id);
            Assert.False((bool)updatedBatch.Enabled);
        }
    }
}

