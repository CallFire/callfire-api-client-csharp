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
    public class TextBroadcastsApiTest : AbstractApiTest
    {
        [Test]
        public void Create()
        {
            var requestJson = GetJsonPayload("/campaigns/textBroadcastsApi/request/createTextBroadcast.json");
            var responseJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/createTextBroadcast.json");
            var restRequest = MockRestResponse(responseJson);

            var textBroadcast = new TextBroadcast
            {
                Name = "Example API SMS",
                FromNumber = "19206596476",
                Message = "Hello World!",
                Recipients = new List<TextRecipient>
                {
                    new TextRecipient { PhoneNumber = "13233832214" },
                    new TextRecipient { PhoneNumber = "13233832215" },
                },
                ResumeNextDay = true
            };
            var id = Client.TextBroadcastsApi.Create(textBroadcast, true, true);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("start") && p.Value.Equals("True")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("strictValidation") && p.Value.Equals("True")));
        }

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/findTextBroadcasts.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindBroadcastsRequest()
            {
                Limit = 5,
                Name = "name",
                Label = "label",
                Running = true
            };
            var broadcasts = Client.TextBroadcastsApi.Find(request);
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
        public void Update()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/request/updateTextBroadcast.json");
            var restRequest = MockRestResponse(expectedJson);

            var textBroadcast = new TextBroadcast
            {
                Id = 11,
                Name = "Example API SMS updated",
                Message = "a new test message",
                ResumeNextDay = true,
            };
            Client.TextBroadcastsApi.Update(textBroadcast, true);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(Serializer.Serialize(requestBodyParam.Value), expectedJson);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11"));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("strictValidation") && p.Value.Equals("True")));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/getTextBroadcast.json");
            var restRequest = MockRestResponse(expectedJson);

            var broadcast = Client.TextBroadcastsApi.Get(11);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
            Assert.That(Serializer.Serialize(broadcast), Is.EqualTo(expectedJson));

            Client.TextBroadcastsApi.Get(11L, FIELDS);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11"));
        }

        [Test]
        public void Start()
        {
            var restRequest = MockRestResponse();
            Client.TextBroadcastsApi.Start(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/10/start"));
        }

        [Test]
        public void Stop()
        {
            var restRequest = MockRestResponse();
            Client.TextBroadcastsApi.Stop(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/10/stop"));
        }

        [Test]
        public void Archive()
        {
            var restRequest = MockRestResponse();
            Client.TextBroadcastsApi.Archive(10L);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/10/archive"));
        }

        [Test]
        public void GetTextsWithGetByIdRequest()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/getTexts.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetByIdRequest
            {
                Offset = 5,
                Fields = FIELDS,
                Id = 11
            };
            var texts = Client.TextBroadcastsApi.GetTexts(request);
            Assert.That(Serializer.Serialize(texts), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11/texts"));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void GetTextsWithGetByIdAndBatchIdRequest()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/getTexts.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetBroadcastCallsTextsRequest
            {
                Offset = 5,
                Fields = FIELDS,
                Id = 11,
                BatchId = 13
            };
            var texts = Client.TextBroadcastsApi.GetTexts(request);
            Assert.That(Serializer.Serialize(texts), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11/texts"));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("batchId") && p.Value.Equals("13")));
        }

        [Test]
        public void GetStats()
        {
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/getStats.json");
            var restRequest = MockRestResponse(expectedJson);

            var stats = Client.TextBroadcastsApi.GetStats(11L);
            Assert.That(Serializer.Serialize(stats), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11/stats"));

            var begin = DateTime.Now.AddDays(-5d);
            var end = DateTime.Now;
            Client.TextBroadcastsApi.GetStats(11L, FIELDS, begin, end);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11/stats"));

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
            var expectedJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/getBatches.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new GetByIdRequest
            {
                Offset = 0,
                Fields = FIELDS,
                Id = 11
            };
            var batches = Client.TextBroadcastsApi.GetBatches(request);
            Assert.That(Serializer.Serialize(batches), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("id")));
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11/batches"));
        }

        [Test]
        public void AddBatch()
        {
            var requestJson = GetJsonPayload("/campaigns/textBroadcastsApi/request/addBatch.json");
            var responseJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/addBatch.json");
            var restRequest = MockRestResponse(responseJson);

            var request = new AddBatchRequest
            {
                CampaignId = 15,
                Name = "contact batch for text",
                ScrubDuplicates = true,
                Recipients = new List<Recipient>
                {
                    new TextRecipient { PhoneNumber = "12135551122" },
                    new TextRecipient { PhoneNumber = "12135551123" },
                },
                StrictValidation = true
            };
            var id = Client.TextBroadcastsApi.AddBatch(request);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("campaignId")));
            Assert.That(restRequest.Value.Resource, Does.EndWith("/15/batches"));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("strictValidation") && p.Value.Equals("True")));
        }

        [Test]
        public void AddRecipients()
        {
            var requestJson = GetJsonPayload("/campaigns/textBroadcastsApi/request/addRecipients.json");
            var responseJson = GetJsonPayload("/campaigns/textBroadcastsApi/response/addRecipients.json");
            var restRequest = MockRestResponse(responseJson);

            var recipients = new List<TextRecipient>
            {
                new TextRecipient { PhoneNumber = "12135551100" },
                new TextRecipient { PhoneNumber = "12135551101" },
            };
            var texts = Client.TextBroadcastsApi.AddRecipients(15, recipients, null, true);
            Assert.That(Serializer.Serialize(new ListHolder<CallfireApiClient.Api.CallsTexts.Model.Text>(texts)), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Resource, Does.EndWith("/15/recipients"));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("strictValidation") && p.Value.Equals("True")));

            Client.TextBroadcastsApi.AddRecipients(15, recipients, FIELDS);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}

