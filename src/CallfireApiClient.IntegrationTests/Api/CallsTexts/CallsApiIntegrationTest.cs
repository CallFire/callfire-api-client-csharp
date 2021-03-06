﻿using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Campaigns.Model;
using System.IO;

namespace CallfireApiClient.IntegrationTests.Api.CallsTexts
{
    [TestFixture]
    public class CallsApiIntegrationTest : AbstractIntegrationTest
    {

        [Test]
        public void GetCall()
        {
            Call call = Client.CallsApi.Get(1930885149003, "id,toNumber,state");
            Console.WriteLine("Call: " + call);

            Assert.AreEqual(1930885149003, call.Id);
            Assert.AreEqual("12132212384", call.ToNumber);
            Assert.AreEqual(StateType.FINISHED, call.State);
        }

        [Test]
        public void FindCalls()
        {
            var request = new FindCallsRequest
            {
                IntervalBegin = DateTime.UtcNow.AddMonths(-10),
                IntervalEnd = DateTime.UtcNow,
                Limit = 1
            };

            Page<Call> calls = Client.CallsApi.Find(request);
            Console.WriteLine("Calls: " + calls);

            Assert.AreEqual(1, calls.Items.Count);
        }

        [Test]
        public void SendCall()
        {
            var contacts = Client.ContactsApi.Find(new FindContactsRequest());

            var recipient1 = new CallRecipient { ContactId = contacts.Items[0].Id, LiveMessage = "testMessage", TransferDigit = "1", TransferMessage = "transferTestMessage" };
            var recipient2 = new CallRecipient { ContactId = contacts.Items[0].Id, LiveMessage = "testMessage", TransferDigit = "1", TransferMessageSoundId = 9643523003 };
            var recipients = new List<CallRecipient> { recipient1, recipient2 };

            IList<Call> calls = Client.CallsApi.Send(recipients, null, "items(id,fromNumber,state)");
            Console.WriteLine("Calls: " + calls);

            Assert.AreEqual(2, calls.Count);
            Assert.NotNull(calls[0].Id);
            Assert.IsNull(calls[0].CampaignId);
            Assert.AreEqual(StateType.READY, calls[0].State);

            var request = new SendCallsRequest
            {
                Recipients = recipients,
                CampaignId = 7373471003,
                Fields = "items(id, fromNumber, state, campaignId)",
                DefaultLiveMessage = "DefaultLiveMessage",
                DefaultMachineMessage = "DefaultMachineMessage",
                DefaultVoice = Voice.FRENCHCANADIAN1,
                StrictValidation = true
            };
            calls = Client.CallsApi.Send(request);
            Assert.AreEqual(2, calls.Count);
            Assert.AreEqual(calls[0].CampaignId, 7373471003);

            request = new SendCallsRequest
            {
                Recipients = recipients,
                CampaignId = 7373471003,
                Fields = "items(id, fromNumber, state, campaignId)",
                DefaultLiveMessageSoundId = 9643523003,
                DefaultMachineMessageSoundId = 9643523003,
                DefaultVoice = Voice.FRENCHCANADIAN1
            };
            calls = Client.CallsApi.Send(request);
            Assert.AreEqual(2, calls.Count);
        }

        [Test]
        public void GetCallRecording()
        {
            CallRecording rec = Client.CallsApi.GetCallRecording(64837518003);
            Assert.NotNull(rec);
            Assert.NotNull(rec.Id);
            Assert.NotNull(rec.Mp3Url);

            rec = Client.CallsApi.GetCallRecording(64837518003, "campaignId");
            Assert.Null(rec.Id);
        }

        [Test]
        public void GetCallRecordingInMp3Format()
        {
            string mp3FilePath = GetFullPath("/Resources/File-examples/testDownloadRecordingById.mp3");
            MemoryStream ms = (MemoryStream)Client.CallsApi.GetCallRecordingMp3(64837518003);
            File.WriteAllBytes(mp3FilePath, ms.ToArray());
        }

        [Test]
        public void GetCallRecordings()
        {
            CallRecording rec = Client.CallsApi.GetCallRecording(64837518003);
            IList<CallRecording> recs = Client.CallsApi.GetCallRecordings((long) rec.CallId, null);
            Assert.NotNull(recs);
            Assert.AreEqual(rec.CallId, recs[0].CallId);

            recs = Client.CallsApi.GetCallRecordings((long)rec.CallId, "items(callId)");
            Assert.Null(recs[0].Id);
            Assert.NotNull(recs[0].CallId);
        }

        [Test]
        public void GetCallRecordingByName()
        {
            CallRecording rec = Client.CallsApi.GetCallRecording(18666772003);
            CallRecording recording = Client.CallsApi.GetCallRecordingByName((long)rec.CallId, rec.Name, null);
            Assert.NotNull(recording);
            Assert.AreEqual(rec.CallId, recording.CallId);
            Assert.AreEqual(rec.Id, recording.Id);
            Assert.AreEqual(rec.Name, recording.Name);
            Assert.AreEqual(rec.Mp3Url, recording.Mp3Url);

            recording = Client.CallsApi.GetCallRecordingByName((long)rec.CallId, rec.Name, "callId");
            Assert.Null(recording.Id);
            Assert.NotNull(recording.CallId);
        }

        [Test]
        public void CallRecordingMp3ByName()
        {
            CallRecording rec = Client.CallsApi.GetCallRecording(18666772003);
            string mp3FilePath = GetFullPath("/Resources/File-examples/testDownloadRecordingByName.mp3");
            MemoryStream ms = (MemoryStream)Client.CallsApi.GetCallRecordingMp3ByName((long) rec.CallId, rec.Name);
            File.WriteAllBytes(mp3FilePath, ms.ToArray());
        }
    }
}
