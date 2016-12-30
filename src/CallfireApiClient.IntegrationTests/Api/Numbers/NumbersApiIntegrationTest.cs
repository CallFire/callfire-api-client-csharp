using System;
using NUnit.Framework;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Numbers.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Numbers
{
    [TestFixture]
    public class NumbersApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindTollfreeNumbers()
        {
            var request = new CommonFindRequest { Limit = 2 };
            var numbers = Client.NumbersApi.FindNumbersTollfree(request);
            Assert.AreEqual(2, numbers.Count);

            Console.WriteLine(numbers);
        }

        [Test]
        public void FindNumbersLocal()
        {
            var request = new FindNumbersLocalRequest { Limit = 2, State = "LA" };
            var numbers = Client.NumbersApi.FindNumbersLocal(request);
            Assert.AreEqual(2, numbers.Count);
            Assert.That(numbers[0].NationalFormat, Is.StringStarting("(225)"));

            Console.WriteLine(numbers);
        }

        [Test]
        public void FindNumberRegions()
        {
            var request = new FindNumberRegionsRequest
            {
                Limit = 2,
                State = "IL",
                Zipcode = "60640"
            };
            var regions = Client.NumbersApi.FindNumberRegions(request);
            Assert.AreEqual(2, regions.Items.Count);
            Assert.That(regions.Items[0].City, Is.StringContaining("CHICAGO"));
            Assert.AreEqual("1773271", regions.Items[0].Prefix);

            Console.WriteLine(regions);
        }
    }
}

