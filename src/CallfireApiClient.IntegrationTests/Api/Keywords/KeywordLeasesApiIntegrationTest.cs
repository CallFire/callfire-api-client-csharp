using System;
using NUnit.Framework;
using CallfireApiClient.Api.Keywords.Model;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.IntegrationTests.Api.Keywords
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
            Assert.AreEqual(keywordLease.Status, LeaseStatus.ACTIVE);

            // update testing
            bool? savedAutoRenew = keywordLease.AutoRenew;
            keywordLease.AutoRenew = !savedAutoRenew;

            if (savedAutoRenew == false)
            {
                var ex1 = Assert.Throws<CallfireApiClient.BadRequestException>(() => Client.KeywordLeasesApi.Update(keywordLease));
                Assert.That(ex1.ApiErrorMessage.Message, Is.StringMatching("Can't change autoRenew once it is false"));
            }
            else
            {
                Client.KeywordLeasesApi.Update(keywordLease);
            }
            
            // get testing with params
            var keywordLeaseUpdated = Client.KeywordLeasesApi.Get(keywordLease.KeywordName, "autoRenew");
            Assert.AreEqual(keywordLeaseUpdated.KeywordName, null);
            Assert.AreEqual(keywordLeaseUpdated.AutoRenew, savedAutoRenew);

            // get back stage before test
            keywordLease.AutoRenew = savedAutoRenew;
           
            var ex2 = Assert.Throws<CallfireApiClient.BadRequestException>(() => Client.KeywordLeasesApi.Update(keywordLease));
            Assert.That(ex2.ApiErrorMessage.Message, Is.StringMatching("Can't change autoRenew once it is false")); 
        }

    }

}

