﻿using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.Campaigns.Model.Request;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;

namespace CallfireApiClient.IntegrationTests.Api.Campaigns
{
    [TestFixture]
    public class CallBroadcastsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void VoiceBroadcastCrudOperations()
        {
            var broadcast = new CallBroadcast
            {
                Name = "call_broadcast1",
                AnsweringMachineConfig = AnsweringMachineConfig.AM_AND_LIVE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is a live answer text to speech recording",
                    LiveSoundTextVoice = Voice.MALE1,
                    MachineSoundText = "This is an answering machine text to speech recording",
                    MachineSoundTextVoice = Voice.MALE1
                },
                Recipients = new List<CallRecipient>
                {
                    new CallRecipient { PhoneNumber = "12132212384" },
                    new CallRecipient { PhoneNumber = "12132212385" }
                },
                ResumeNextDay = true
            };
            var id = Client.CallBroadcastsApi.Create(broadcast, true, true);
            System.Console.WriteLine("broadcast id: " + id);
            var savedBroadcast = Client.CallBroadcastsApi.Get(id.Id);
            Assert.AreEqual(broadcast.Name, savedBroadcast.Name);
            Assert.AreEqual(savedBroadcast.ResumeNextDay, true);
            savedBroadcast.Name = "updated_name";
            savedBroadcast.Sounds.LiveSoundText = null;
            savedBroadcast.Sounds.MachineSoundText = null;
            savedBroadcast.ResumeNextDay = false;
            Client.CallBroadcastsApi.Update(savedBroadcast);

            var updatedBroadcast = Client.CallBroadcastsApi.Get(id.Id, "id,name,resumeNextDay");
            Assert.Null(updatedBroadcast.Status);
            Assert.NotNull(updatedBroadcast.Id);
            Assert.AreEqual(savedBroadcast.Name, updatedBroadcast.Name);
            Assert.AreEqual(updatedBroadcast.ResumeNextDay, false);
        }

        [Test]
        public void IvrsCrudOperations()
        {
            var broadcast = new CallBroadcast
            {
                Name = "ivr_broadcast1",
                DialplanXml = "<dialplan name=\"Root\"></dialplan>",
                Recipients = new List<CallRecipient>
                {
                    new CallRecipient { PhoneNumber = "12132212384" },
                    new CallRecipient { PhoneNumber = "12132212385" }
                }
            };
            var id = Client.CallBroadcastsApi.Create(broadcast, true);
            System.Console.WriteLine("ivr id: " + id);
            var savedBroadcast = Client.CallBroadcastsApi.Get(id.Id);
            Assert.AreEqual(broadcast.Name, savedBroadcast.Name);

            savedBroadcast.Name = "updated_name";
            savedBroadcast.DialplanXml = "<dialplan name=\"Root\">\r\n\t<play type=\"tts\">Congratulations! You have successfully configured a CallFire I V R.</play>\r\n</dialplan>";
            Client.CallBroadcastsApi.Update(savedBroadcast, true);

            var updatedBroadcast = Client.CallBroadcastsApi.Get(id.Id, "id,name");
            Assert.Null(updatedBroadcast.Status);
            Assert.NotNull(updatedBroadcast.Id);
            Assert.AreEqual(savedBroadcast.Name, updatedBroadcast.Name);
        }

        [Test]
        public void StartStopArchiveCampaign()
        {
            var broadcast = new CallBroadcast
            {
                Name = "call_broadcast",
                AnsweringMachineConfig = AnsweringMachineConfig.AM_AND_LIVE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is a live answer text to speech recording",
                    LiveSoundTextVoice = Voice.MALE1,
                    MachineSoundText = "This is an answering machine text to speech recording",
                    MachineSoundTextVoice = Voice.MALE1
                },
                Recipients = new List<CallRecipient>
                {
                    new CallRecipient { PhoneNumber = "12132041238" },
                    new CallRecipient { PhoneNumber = "14246525473" }
                }
            };
            var id = Client.CallBroadcastsApi.Create(broadcast, false);

            CallBroadcast campaign = Client.CallBroadcastsApi.Get(id.Id);
            System.Console.WriteLine(campaign);
            Assert.NotNull(campaign);
            // start
            Client.CallBroadcastsApi.Start((long)campaign.Id);
            campaign = Client.CallBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.RUNNING, campaign.Status);
            // stop
            Client.CallBroadcastsApi.Stop((long)campaign.Id);
            campaign = Client.CallBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.STOPPED, campaign.Status);
            // archive
            Client.CallBroadcastsApi.Archive((long)campaign.Id);
            campaign = Client.CallBroadcastsApi.Get((long)campaign.Id, "id,status");
            Assert.AreEqual(BroadcastStatus.ARCHIVED, campaign.Status);
        }

        [Test]
        public void GetBroadcastCalls()
        {
            var broadcast = new CallBroadcast
            {
                Name = "call_broadcast",
                AnsweringMachineConfig = AnsweringMachineConfig.AM_AND_LIVE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is a live answer text to speech recording",
                    LiveSoundTextVoice = Voice.MALE1,
                    MachineSoundText = "This is an answering machine text to speech recording",
                    MachineSoundTextVoice = Voice.MALE1
                },
                Recipients = new List<CallRecipient>
                {
                    new CallRecipient { PhoneNumber = "12132041238" },
                    new CallRecipient { PhoneNumber = "14246525473" }
                }
            };
            var id = Client.CallBroadcastsApi.Create(broadcast, false);

            var getCallsRequest = new GetByIdRequest { Id = id.Id };
            var calls = Client.CallBroadcastsApi.GetCalls(getCallsRequest);
            System.Console.WriteLine(calls);
            Assert.That(calls.Items, Is.Not.Empty);

            long testBatchId = (long) calls.Items[0].BatchId;

            var getCallsRequestNew = new GetBroadcastCallsTextsRequest { Id = id.Id, BatchId = testBatchId };
            calls = Client.CallBroadcastsApi.GetCalls(getCallsRequestNew);
            System.Console.WriteLine(calls);
            Assert.AreEqual(calls.Items[0].BatchId, testBatchId);
        }

        [Test]
        public void GetBroadcastStats()
        {
            var begin = System.DateTime.Now.AddDays(-5d);
            var end = System.DateTime.Now;
            var fields = "callsAttempted,callsPlaced,callsDuration";
            var stats = Client.CallBroadcastsApi.GetStats(7373471003, fields, begin, end);
            System.Console.WriteLine(stats);
        }

        [Test]
        public void AddRecipientsAndAddRemoveBatches()
        {
            var findRequest = new FindBroadcastsRequest
            {
                Name = "updated_name",
                Limit = 1
            };
            var broadcasts = Client.CallBroadcastsApi.Find(findRequest);
            System.Console.WriteLine(broadcasts);
            Assert.That(broadcasts.Items, Is.Not.Empty);
            var id = broadcasts.Items[0].Id;

            var calls = Client.CallBroadcastsApi.AddRecipients((long)id, new List<Recipient>
                {
                    new Recipient { PhoneNumber = "12132212384" },
                    new Recipient { PhoneNumber = "12132212385" }
                }, null, true);
            System.Console.WriteLine(calls);
            Assert.AreEqual(2, calls.Count);

            // get batches
            var getBatchesRequest = new GetByIdRequest { Id = id };
            var batches = Client.CallBroadcastsApi.GetBatches(getBatchesRequest);
            System.Console.WriteLine(batches);

            // add batch
            var addBatchRequest = new AddBatchRequest
            {
                CampaignId = (long)id,
                Name = "new_batch",
                Recipients = new List<Recipient>
                {
                    new Recipient { PhoneNumber = "12132212386" },
                    new Recipient { PhoneNumber = "12132212387" }
                },
                StrictValidation = true
            };
            ResourceId addedBatchId = Client.CallBroadcastsApi.AddBatch(addBatchRequest);

            var addedBatch = Client.BatchesApi.Get(addedBatchId.Id);
            Assert.AreEqual(addedBatch.BroadcastId, id);
        }

        [Test]
        public void ToggleRecipientsStatus()
        {
            var broadcast = new CallBroadcast
            {
                Name = "call_broadcast1",
                AnsweringMachineConfig = AnsweringMachineConfig.AM_AND_LIVE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is a live answer text to speech recording",
                    LiveSoundTextVoice = Voice.MALE1,
                    MachineSoundText = "This is an answering machine text to speech recording",
                    MachineSoundTextVoice = Voice.MALE1
                },
                Recipients = new List<CallRecipient>
                {
                    new CallRecipient { PhoneNumber = "12132212384" },
                    new CallRecipient { PhoneNumber = "12132212385" }
                },
                ResumeNextDay = true
            };
            var id = Client.CallBroadcastsApi.Create(broadcast, false, false);
            System.Console.WriteLine("broadcast id: " + id);

            var getCallsRequest = new GetByIdRequest { Id = id.Id };
            var calls = Client.CallBroadcastsApi.GetCalls(getCallsRequest);
            Assert.That(calls.Items, Is.Not.Empty);
            foreach (Call c in calls.Items)
            {
                Assert.AreEqual(StateType.READY, c.State);
            }

            var recipients = new List<Recipient>
            {
                new Recipient { PhoneNumber = "12132212384" },
                new Recipient { PhoneNumber = "12132212385" }
            };

            Client.CallBroadcastsApi.ToggleRecipientsStatus(id.Id, recipients, false);

            calls = Client.CallBroadcastsApi.GetCalls(getCallsRequest);
            foreach (Call c in calls.Items)
            {
                Assert.AreEqual(StateType.DISABLED, c.State);
            }

            Client.CallBroadcastsApi.ToggleRecipientsStatus(id.Id, recipients, true);

            calls = Client.CallBroadcastsApi.GetCalls(getCallsRequest);
            foreach (Call c in calls.Items)
            {
                Assert.AreEqual(StateType.READY, c.State);
            }
        }
    }
}

