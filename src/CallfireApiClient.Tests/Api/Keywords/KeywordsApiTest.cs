using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Keywords.Model;
using System;

namespace CallfireApiClient.Tests.Api.Keywords
{
    [TestFixture]
    public class KeywordsTest : AbstractApiTest
    {
        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/keywords/keywordsApi/response/findKeywords.json");
            MockRestResponse(expectedJson);
            IList<Keyword> keywords = Client.KeywordsApi.Find(new List<string>(new string[] { TEST_STRING, TEST_STRING }));
            Assert.NotNull(keywords);
            Assert.That(Serializer.Serialize(new ListHolder<Keyword> (keywords)), Is.EqualTo(expectedJson));
        }

        [Test]
        public void IsAvailable()
        { 
            MockRestResponse("true");
            Boolean isAvailable = Client.KeywordsApi.IsAvailable(TEST_STRING);
            Assert.AreEqual(isAvailable, true);
        }

    }
}

