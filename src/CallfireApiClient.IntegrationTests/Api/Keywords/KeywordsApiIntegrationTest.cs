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
            string KW1 = "TEST1";
            string KW2 = "TEST2";

            IList<string> keywordsNames = new List<string> { KW1, KW2 };
            var keywords = Client.KeywordsApi.Find(keywordsNames);

            foreach (Keyword keyword in keywords)
            {
                Console.WriteLine(keyword.ToString());
            }

            Assert.AreEqual(2, keywords.Count);
            Assert.AreEqual(KW1, keywords[0].KeywordName);
            Assert.AreEqual(KW2, keywords[1].KeywordName);
        }

        [Test]
        public void IsAvailable()
        {
            Boolean isAvaialble = Client.KeywordsApi.IsAvailable("TEST");
            Assert.AreEqual(true, isAvaialble);
        }

    }

}

