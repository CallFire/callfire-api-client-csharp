using NUnit.Framework;
using CallfireApiClient.Api.Keywords.Model.Request;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Numbers.Model.Request;

namespace CallfireApiClient.Tests.Api.Account
{
    [TestFixture]
    public class OrdersApiTest : AbstractApiTest
    {
        [Test]
        public void OrderKeywords()
        {
            string requestJson = GetJsonPayload("/account/ordersApi/request/orderKeywords.json");
            string responseJson = GetJsonPayload("/account/ordersApi/response/orderKeywords.json");
            var restRequest = MockRestResponse(responseJson);

            var request = new KeywordPurchaseRequest { Keywords = { "KW1", "KW2" } };
            var id = Client.OrdersApi.OrderKeywords(request);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
        }

        [Test]
        public void OrderNumbers()
        {
            string requestJson = GetJsonPayload("/account/ordersApi/request/orderNumbers.json");
            string responseJson = GetJsonPayload("/account/ordersApi/response/orderNumbers.json");
            var restRequest = MockRestResponse(responseJson);

            var request = new NumberPurchaseRequest { LocalCount = 2, Zipcode = "90401" };
            var id = Client.OrdersApi.OrderNumbers(request);
            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
        }

        [Test]
        public void GetOrder()
        {
            string expectedJson = GetJsonPayload("/account/ordersApi/response/getOrder.json");
            var restRequest = MockRestResponse(expectedJson);

            var numberOrder = Client.OrdersApi.GetOrder(1L);
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));   

            numberOrder = Client.OrdersApi.GetOrder(1L, FIELDS);
            Assert.That(Serializer.Serialize(numberOrder), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }
    }
}