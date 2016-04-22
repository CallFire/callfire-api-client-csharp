using System;
using NUnit.Framework;
using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Tests.Integration.Keywords
{
    [TestFixture, Ignore("temporary disabled")]
    public class KeywordLeasesApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void Find()
        {
            var request = new CommonFindRequest();
            Page<KeywordLease> keywordLeases = Client.KeywordLeasesApi.Find(request);
            Assert.NotNull(keywordLeases);
            Console.WriteLine("Page of keywordLeases:" + keywordLeases);
        }

        [Test]
        public void GetUpdateKeywordLease()
        {
            // get testing
            var keywordLease = Client.KeywordLeasesApi.Get("TEST_KEYWORD");
            Assert.AreEqual(keywordLease.KeywordName, "TEST_KEYWORD");
            Assert.IsTrue(keywordLease.Status.Equals(LeaseStatus.PENDING) || keywordLease.Status.Equals(LeaseStatus.ACTIVE));

            // update testing
            Client.KeywordLeasesApi.Update(keywordLease);
            
            // get testing with params
            var keywordLeaseUpdated = Client.KeywordLeasesApi.Get(keywordLease.KeywordName, "autoRenew");
            Assert.Null(keywordLeaseUpdated.KeywordName);
        }
    }
}

