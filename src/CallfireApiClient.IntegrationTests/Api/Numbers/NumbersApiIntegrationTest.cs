﻿using System;
using NUnit.Framework;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Numbers.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Numbers
{
    [TestFixture]
    public class NumbersApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindObsoleteTollfreeNumbers()
        {
            var request = new CommonFindRequest { Limit = 2 };
            var numbers = Client.NumbersApi.FindNumbersTollfree(request);
            Assert.AreEqual(2, numbers.Count);

            Console.WriteLine(numbers);
        }

        [Test]
        public void FindTollfreeNumbers()
        {
            var request = new FindTollfreeNumbersRequest
            {
                Limit = 2,
                Pattern = "84*",
                Fields = "items(number)"
            };
            var numbers = Client.NumbersApi.FindNumbersTollfree(request);
            Assert.AreEqual(2, numbers.Count);
            Assert.True(numbers[0].PhoneNumber.Contains("84"));
            Assert.True(numbers[1].PhoneNumber.Contains("84"));
            Assert.Null(numbers[0].NationalFormat);
            Assert.Null(numbers[0].Region);
            Console.WriteLine(numbers);
        }

        [Test]
        public void FindNumbersLocal()
        {
            var request = new FindNumbersLocalRequest { Limit = 1, State = "LA" };
            var numbers = Client.NumbersApi.FindNumbersLocal(request);
            Assert.AreEqual(1, numbers.Count);
            Assert.NotNull(numbers[0].NationalFormat);
            Assert.NotNull(numbers[0].PhoneNumber);
            Assert.NotNull(numbers[0].Region);
            Assert.NotNull(numbers[0].TollFree);
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
            Assert.That(regions.Items[0].City, Does.Contain("CHICAGO"));
            Assert.AreEqual("1773271", regions.Items[0].Prefix);

            Console.WriteLine(regions);
        }
    }
}

