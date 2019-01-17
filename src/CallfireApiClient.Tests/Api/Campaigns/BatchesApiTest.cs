using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using RestSharp;
using System.Linq;

namespace CallfireApiClient.Tests.Api.Campaigns
{
    [TestFixture]
    public class BatchesApiTest : AbstractApiTest
    {
        [Test]
        public void Get()
        {
            string expectedJson = GetJsonPayload("/campaigns/batchesApi/response/getBatch.json");
            var restRequest = MockRestResponse(expectedJson);

            Batch batch = Client.BatchesApi.Get(11L, FIELDS);
            Assert.That(Serializer.Serialize(batch), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   

            Client.BatchesApi.Get(11L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   
        }

        [Test]
        public void Update()
        {
            String requestJson = GetJsonPayload("/campaigns/batchesApi/request/updateBatch.json");
            var restRequest = MockRestResponse();
            var batch = new Batch
            {
                Id = 11,
                Enabled = false
            };
            Client.BatchesApi.Update(batch);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(Serializer.Serialize(requestBodyParam.Value), requestJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void UpdateNullId()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Client.BatchesApi.Update(new Batch()));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
            Assert.That(ex.Message, Is.StringContaining("Parameter name: batch.id"));
        }
    }
}

