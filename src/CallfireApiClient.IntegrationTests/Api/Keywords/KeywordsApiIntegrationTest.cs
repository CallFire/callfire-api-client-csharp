using System;
using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Keywords.Model;

namespace CallfireApiClient.IntegrationTests.Api.Keywords
{
    [TestFixture]
    public class KeywordsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindKeywords()
        {
            string KW = "TEST_KEYWORD";

            var keywords = Client.KeywordsApi.Find(new List<string> { KW });

            Assert.AreEqual(1, keywords.Count);
            Assert.AreEqual(KW, keywords[0].KeywordName);
        }

        [Test]
        public void IsAvailable()
        {
            Boolean isAvaialble = Client.KeywordsApi.IsAvailable("TEST");
            Assert.AreEqual(true, isAvaialble);
        }

    }

}

