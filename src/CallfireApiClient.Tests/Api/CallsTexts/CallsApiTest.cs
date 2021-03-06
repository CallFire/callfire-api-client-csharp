﻿using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Campaigns.Model;
using System;

namespace CallfireApiClient.Tests.Api.CallsTexts
{
    [TestFixture]
    public class CallsApiTest : AbstractApiTest
    {
        private const string EMPTY_KEY_MSG = "recordingName cannot be blank";

        [Test]
        public void SendCalls()
        {
            string requestJson = GetJsonPayload("/callstexts/callsApi/request/sendCalls.json");
            string responseJson = GetJsonPayload("/callstexts/callsApi/response/sendCalls.json");
            var restRequest = MockRestResponse(responseJson);

            CallRecipient r1 = new CallRecipient { PhoneNumber = "12135551100", LiveMessage = "Why hello there!", TransferDigit = "1", TransferMessage = "testMessage", TransferNumber = "12135551101" };
            CallRecipient r2 = new CallRecipient { PhoneNumber = "12135551101", LiveMessage = "And hello to you too.", FromNumber = "12135551102" };
            IList<Call> calls = Client.CallsApi.Send(new List<CallRecipient> { r1, r2 });

            Assert.That(Serializer.Serialize(new ListHolder<Call>(calls)), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));

            calls = Client.CallsApi.Send(new List<CallRecipient> { r1, r2 }, 10, FIELDS);
            Assert.That(restRequest.Value.Resource, !Does.Contain("fields" + FIELDS));
            Assert.That(restRequest.Value.Resource, !Does.Contain("campaignId=100"));
        }

        [Test]
        public void SendCallsUsingRequest()
        {
            string requestJson = GetJsonPayload("/callstexts/callsApi/request/sendCalls.json");
            string responseJson = GetJsonPayload("/callstexts/callsApi/response/sendCalls.json");
            var restRequest = MockRestResponse(responseJson);

            CallRecipient r1 = new CallRecipient { PhoneNumber = "12135551100", LiveMessage = "Why hello there!", TransferDigit = "1", TransferMessage = "testMessage", TransferNumber = "12135551101" };
            CallRecipient r2 = new CallRecipient { PhoneNumber = "12135551101", LiveMessage = "And hello to you too.", FromNumber = "12135551102" };

            var request = new SendCallsRequest
            {
                Recipients = new List<CallRecipient> { r1, r2 },
                CampaignId = 10,
                Fields = FIELDS,
                DefaultLiveMessage = "DefaultLiveMessage",
                DefaultMachineMessage = "DefaultMachineMessage",
                DefaultLiveMessageSoundId = 1,
                DefaultMachineMessageSoundId = 1,
                DefaultVoice = Voice.FRENCHCANADIAN1,
                StrictValidation = true
            };

            IList<Call> calls = Client.CallsApi.Send(request);

            Assert.That(Serializer.Serialize(new ListHolder<Call>(calls)), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));

            calls = Client.CallsApi.Send(new List<CallRecipient> { r1, r2 }, 10, FIELDS);
            Assert.That(restRequest.Value.Resource, !Does.Contain("fields" + FIELDS));
            Assert.That(restRequest.Value.Resource, !Does.Contain("campaignId=10"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultLiveMessage=DefaultLiveMessage"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultMachineMessage=DefaultMachineMessage"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultLiveMessageSoundId=1"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultMachineMessageSoundId=1"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultMachineMessageSoundId=1"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("defaultVoice=FRENCHCANADIAN1"));
            Assert.That(restRequest.Value.Resource, !Does.Contain("strictValidation=TRUE"));
        }

        [Test]
        public void FindCalls()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/findCalls.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new FindCallsRequest
            {
                States = new List<StateType> { StateType.CALLBACK, StateType.DISABLED },
                Id = new List<long> { 1, 2, 3 },
                Limit = 5,
                Offset = 0,
                BatchId = 1001
            };

            Page<Call> calls = Client.CallsApi.Find(request);

            Assert.That(Serializer.Serialize(calls), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("batchId") && p.Value.Equals("1001")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("2")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("3")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("CALLBACK")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("states") && p.Value.Equals("DISABLED")));
        }

        [Test]
        public void GetCall()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCall.json");
            var restRequest = MockRestResponse(expectedJson);

            Call call = Client.CallsApi.Get(1); 
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));

            call = Client.CallsApi.Get(1, FIELDS);
            Assert.That(Serializer.Serialize(call), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void GetCallRecordings()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecordings.json");
            var restRequest = MockRestResponse(expectedJson);

            IList<CallRecording> callRecordings = Client.CallsApi.GetCallRecordings(10, null);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));
            Assert.That(Serializer.Serialize(new ListHolder<CallRecording>(callRecordings)), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.Contain("/calls/10/recordings"));
        }

        [Test]
        public void GetCallRecordingsWithFieldsParam()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecordings.json");
            var restRequest = MockRestResponse(expectedJson);

            IList<CallRecording>  callRecordings = Client.CallsApi.GetCallRecordings(10, FIELDS);
            Assert.That(Serializer.Serialize(new ListHolder<CallRecording>(callRecordings)), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Does.Contain("/calls/10/recordings"));
        }

        [Test]
        public void GetCallRecordingByName()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecording.json");
            var restRequest = MockRestResponse(expectedJson);

            CallRecording callRecording = Client.CallsApi.GetCallRecordingByName(10, "testName", null);
            Assert.That(Serializer.Serialize(callRecording), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Does.Contain("/10/recordings/testName"));
        }

        [Test]
        public void GetCallRecordingByNameWithFieldsParam()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecording.json");
            var restRequest = MockRestResponse(expectedJson);

            CallRecording callRecording = Client.CallsApi.GetCallRecordingByName(10, "testName", FIELDS);
            Console.WriteLine(Serializer.Serialize(callRecording));

            Assert.That(Serializer.Serialize(callRecording), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Does.Contain("/calls/10/recordings/testName"));
        }

        [Test]
        public void GetCallRecordingByNameWithNullNameParameter()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.CallsApi.GetCallRecordingByName(10, null));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));

            ex = Assert.Throws<ArgumentException>(() => Client.CallsApi.GetCallRecordingByName(10, ""));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));
        }

        [Test]
        public void GetMp3CallRecordingByNameWithNullNameParameter()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.CallsApi.GetCallRecordingMp3ByName(10, null));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));

            ex = Assert.Throws<ArgumentException>(() => Client.CallsApi.GetCallRecordingMp3ByName(10, ""));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));
        }

        [Test]
        public void GetCallRecording()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecording.json");
            var restRequest = MockRestResponse(expectedJson);

            CallRecording callRecording = Client.CallsApi.GetCallRecording(10, null);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));
            Assert.That(Serializer.Serialize(callRecording), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.Contain("/calls/recordings/10"));
        }

        [Test]
        public void GetCallRecordingWithFieldsParam()
        {
            string expectedJson = GetJsonPayload("/callstexts/callsApi/response/getCallRecording.json");
            var restRequest = MockRestResponse(expectedJson);

            CallRecording callRecording = Client.CallsApi.GetCallRecording(10, FIELDS);
            Assert.That(Serializer.Serialize(callRecording), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.Contain("/calls/recordings/10"));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}
