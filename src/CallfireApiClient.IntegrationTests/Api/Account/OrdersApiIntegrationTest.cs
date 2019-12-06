using System;
using NUnit.Framework;
using CallfireApiClient.Api.Keywords.Model.Request;
using CallfireApiClient.Api.Numbers.Model.Request;
using System.Collections.Generic;

namespace CallfireApiClient.IntegrationTests.Api.Account
{
    [TestFixture]
    public class OrdersApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void OrderKeywords()
        {
            var request = new KeywordPurchaseRequest { Keywords = { "TEST1", "TEST2" } };
            Assert.That(() => Client.OrdersApi.OrderKeywords(request), 
                Throws.TypeOf<BadRequestException>().With.Property("ApiErrorMessage").With.Property("HttpStatusCode").EqualTo(400)
                .And.Property("Message").StringContaining("no valid credit card on file"));
            Assert.Throws<ResourceNotFoundException>(() => Client.OrdersApi.GetOrder(123));
        }

        [Test]
        public void OrderNumbers()
        {
            var request = new NumberPurchaseRequest { Numbers = new List<string> { "12132212289" }, Zipcode = "90401", LocalCount = 2 };
            Assert.That(() => Client.OrdersApi.OrderNumbers(request), 
                Throws.TypeOf<BadRequestException>().With.Property("ApiErrorMessage").With.Property("HttpStatusCode").EqualTo(400)
                .And.Property("Message").StringContaining("unavailable numbers: 12132212289"));
            Assert.Throws<ResourceNotFoundException>(() => Client.OrdersApi.GetOrder(123));
        }
    }
}

