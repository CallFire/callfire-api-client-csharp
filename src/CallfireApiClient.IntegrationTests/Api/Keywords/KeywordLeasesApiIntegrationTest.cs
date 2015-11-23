using System;
using NUnit.Framework;
using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.IntegrationTests.Api.Keywords
{
    [TestFixture]
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
            ///get testing
            var keywordLease = Client.KeywordLeasesApi.Get("TEST_KEYWORD");
            Assert.AreEqual(keywordLease.keyword, "TEST_KEYWORD");
            Assert.AreEqual(keywordLease.status, LeaseStatus.ACTIVE);

            ///update testing
            bool? savedAutoRenew = keywordLease.autoRenew;
            keywordLease.autoRenew = !savedAutoRenew;

            if (savedAutoRenew == false)
            {
                var ex1 = Assert.Throws<CallfireApiClient.BadRequestException>(() => Client.KeywordLeasesApi.Update(keywordLease));
                Assert.That(ex1.ApiErrorMessage.Message, Is.StringMatching("Can't change autoRenew once it is false"));
            } else
            {
                Client.KeywordLeasesApi.Update(keywordLease);
            }
            
            ///get testing with params
            var keywordLeaseUpdated = Client.KeywordLeasesApi.Get(keywordLease.keyword, "autoRenew");
            Assert.AreEqual(keywordLeaseUpdated.keyword, null);
            Assert.AreEqual(keywordLeaseUpdated.autoRenew, savedAutoRenew);

            ///get back stage before test
            keywordLease.autoRenew = savedAutoRenew;
           
            var ex2 = Assert.Throws<CallfireApiClient.BadRequestException>(() => Client.KeywordLeasesApi.Update(keywordLease));
            Assert.That(ex2.ApiErrorMessage.Message, Is.StringMatching("Can't change autoRenew once it is false")); 
        }

    }

}

