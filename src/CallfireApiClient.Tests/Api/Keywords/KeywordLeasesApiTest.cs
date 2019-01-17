using NUnit.Framework;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model.Request;
using RestSharp;
using System;
using System.Linq;

namespace CallfireApiClient.Tests.Api.Keywords
{
    
    [TestFixture]
    public class KeywordLeasesApiTest : AbstractApiTest
    {
        private const string EMPTY_KEYWORD_MSG = "keyword cannot be blank";
        private const string EMPTY_LEASE_KEYWORD_MSG = "keyword in keywordLease cannot be null";

        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/keywords/keywordLeasesApi/response/findKeywordLeases.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new CommonFindRequest
            {
                Limit = 5L,
                Offset = 0L, 
                Fields = FIELDS
            };
            Page<KeywordLease> keywordLeases = Client.KeywordLeasesApi.Find(request);
            Assert.That(Serializer.Serialize(keywordLeases), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void Get()
        {
            string expectedJson = GetJsonPayload("/keywords/keywordLeasesApi/response/getKeywordLease.json");
            var restRequest = MockRestResponse(expectedJson);

            KeywordLease keywordLease = Client.KeywordLeasesApi.Get(TEST_STRING, FIELDS);
            Assert.That(Serializer.Serialize(keywordLease), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_STRING));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));

            Client.KeywordLeasesApi.Get(TEST_STRING);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_STRING));
        }

        [Test]
        public void GetByNullKeyword()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordLeasesApi.Get(null));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEYWORD_MSG));
        }

        [Test]
        public void Update()
        {
            string requestJson = GetJsonPayload("/keywords/keywordLeasesApi/request/updateKeywordLease.json");
            var restRequest = MockRestResponse();
        
            var keywordLease = new KeywordLease
            {
                KeywordName = TEST_STRING,
                AutoRenew = false
            };
            Client.KeywordLeasesApi.Update(keywordLease);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/keywords/leases/" + TEST_STRING));
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
        }

        [Test]
        public void UpdateByNullKeywordLeaseKeywordValue()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.KeywordLeasesApi.Update(new KeywordLease()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_LEASE_KEYWORD_MSG));
        }

    }

}

