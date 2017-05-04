using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.Campaigns.Model.Request;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model.Request;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Tests.Api.Campaigns
{
    [TestFixture]
    public class CallBroadcastsApiTest : AbstractApiTest
    {
        [Test]
        public void CreateVoiceBroadcast()
        {
            var requestJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/createCallBroadcast.json");
            var responseJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/createCallBroadcast.json");
            var restRequest = MockRestResponse(responseJson);
 
            var callBroadcast = new CallBroadcast
            {
                Name = "Example API VB",
                FromNumber = "12135551189",
                AnsweringMachineConfig = AnsweringMachineConfig.AM_AND_LIVE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is a live answer text to speech recording",
                    LiveSoundTextVoice = Voice.MALE1,
                    MachineSoundText = "This is an answering machine text to speech recording",
                    MachineSoundTextVoice = Voice.MALE1 
                },
                Recipients = new List<Recipient>
                {
                    new Recipient { PhoneNumber = "13233832214" },
                    new Recipient { PhoneNumber = "13233832215" },
                },
                ResumeNextDay = true
            };
            var id = Client.CallBroadcastsApi.Create(callBroadcast, true);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("start") && p.Value.Equals("True")));
        }

        [Test]
        public void CreateIvrBroadcast()
        {
            var requestJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/createIvrBroadcast.json");
            var responseJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/createIvrBroadcast.json");
            var restRequest = MockRestResponse(responseJson);

            var callBroadcast = new CallBroadcast
            {
                Name = "Example API IVR",
                FromNumber = "12135551189",
                DialplanXml = "<dialplan name=\"Root\"></dialplan>",
                Recipients = new List<Recipient>
                {
                    new Recipient { PhoneNumber = "13233832214" },
                    new Recipient { PhoneNumber = "13233832215" },
                }
            };
            var id = Client.CallBroadcastsApi.Create(callBroadcast, true);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("start") && p.Value.Equals("True")));
        }

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/findCallBroadcasts.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindBroadcastsRequest()
            {
                Limit = 5,
                Name = "name",
                Label = "label",
                Running = true
            };
            var broadcasts = Client.CallBroadcastsApi.Find(request);
            Assert.That(Serializer.Serialize(broadcasts), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("name") && p.Value.Equals("name")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("label") && p.Value.Equals("label")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("running") && p.Value.Equals("True")));
        }

        [Test]
        public void UpdateVoiceBroadcast()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/updateCallBroadcast.json");
            var restRequest = MockRestResponse(expectedJson);

            var callBroadcast = new CallBroadcast
            {
                Id = 11,
                Name = "Example API VB updated",
                FromNumber = "12135551189",
                AnsweringMachineConfig = AnsweringMachineConfig.LIVE_IMMEDIATE,
                Sounds = new CallBroadcastSounds
                {
                    LiveSoundText = "Hello! This is an updated VB config tts",
                    MachineSoundId = 1258704003
                },
                ResumeNextDay = true
            };
            Client.CallBroadcastsApi.Update(callBroadcast);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, expectedJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void UpdateIvrBroadcast()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/updateIvrBroadcast.json");
            var restRequest = MockRestResponse(expectedJson);

            var callBroadcast = new CallBroadcast
            {
                Id = 12,
                Name = "Example API IVR updated",
                FromNumber = "12135551189",
                DialplanXml = "<dialplan name=\"Root\">\r\n\t<play type=\"tts\">Congratulations! You have successfully configured a CallFire I V R.</play>\r\n</dialplan>"
            };
            Client.CallBroadcastsApi.Update(callBroadcast);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, expectedJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/12"));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/getCallBroadcast.json");
            var restRequest = MockRestResponse(expectedJson);

            var broadcast = Client.CallBroadcastsApi.Get(11);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
            Assert.That(Serializer.Serialize(broadcast), Is.EqualTo(expectedJson));

            Client.CallBroadcastsApi.Get(11L, FIELDS);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void Start()
        {
            var restRequest = MockRestResponse();
            Client.CallBroadcastsApi.Start(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/10/start"));
        }

        [Test]
        public void Stop()
        {
            var restRequest = MockRestResponse();
            Client.CallBroadcastsApi.Stop(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/10/stop"));
        }

        [Test]
        public void Archive()
        {
            var restRequest = MockRestResponse();
            Client.CallBroadcastsApi.Archive(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/10/archive"));
        }

        [Test]
        public void GetCallsWithGetByIdRequest()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/getCalls.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetByIdRequest
            {
                Offset = 5,
                Fields = FIELDS,
                Id = 11
            };
            var calls = Client.CallBroadcastsApi.GetCalls(request);
            Assert.That(Serializer.Serialize(calls), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11/calls"));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void GetCallsWithGetByIdAndBatchIdRequest()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/getCalls.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetBroadcastCallsTextsRequest
            {
                Offset = 5,
                Fields = FIELDS,
                Id = 11,
                BatchId = 13
            };
            var calls = Client.CallBroadcastsApi.GetCalls(request);
            Assert.That(Serializer.Serialize(calls), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11/calls"));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("batchId") && p.Value.Equals("13")));
        }

        [Test]
        public void GetStats()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/getStats.json");
            var restRequest = MockRestResponse(expectedJson);

            var stats = Client.CallBroadcastsApi.GetStats(11L);
            Assert.That(Serializer.Serialize(stats), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11/stats"));

            var begin = DateTime.Now.AddDays(-5d);
            var end = DateTime.Now;
            Client.CallBroadcastsApi.GetStats(11L, FIELDS, begin, end);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11/stats"));

            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields")
                    && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("begin")
                    && p.Value.Equals(ToUnixTime(begin).ToString())));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("end")
                    && p.Value.Equals(ToUnixTime(end).ToString())));
        }

        [Test]
        public void GetBatches()
        {
            var expectedJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/getBatches.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetByIdRequest
            {
                Offset = 0,
                Fields = FIELDS,
                Id = 11
            };
            var batches = Client.CallBroadcastsApi.GetBatches(request);
            Assert.That(Serializer.Serialize(batches), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11/batches"));
        }

        [Test]
        public void AddBatch()
        {
            var requestJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/addBatch.json");
            var responseJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/addBatch.json");
            var restRequest = MockRestResponse(responseJson);

            var request = new AddBatchRequest
            {
                CampaignId = 15,
                Name = "batch name",
                Recipients = new List<Recipient>
                {
                    new Recipient { PhoneNumber = "12135551100" },
                    new Recipient { PhoneNumber = "12135551101" },
                }
            };
            var id = Client.CallBroadcastsApi.AddBatch(request);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("campaignId")));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/15/batches"));
        }

        [Test]
        public void AddRecipients()
        {
            var requestJson = GetJsonPayload("/campaigns/callBroadcastsApi/request/addRecipients.json");
            var responseJson = GetJsonPayload("/campaigns/callBroadcastsApi/response/addRecipients.json");
            var restRequest = MockRestResponse(responseJson);

            var recipients = new List<Recipient>
            {
                new Recipient { PhoneNumber = "12135551100" },
                new Recipient { PhoneNumber = "12135551101" },
            };
            var calls = Client.CallBroadcastsApi.AddRecipients(15, recipients);
            Assert.That(Serializer.Serialize(new ListHolder<Call>(calls)), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/15/recipients"));

            Client.CallBroadcastsApi.AddRecipients(15, recipients, FIELDS);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}

