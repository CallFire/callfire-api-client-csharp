using NUnit.Framework;
using CallfireApiClient.Api.Numbers.Model.Request;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Numbers.Model;

namespace CallfireApiClient.Tests.Api.Numbers
{
    [TestFixture]
    public class NumbersApiTest : AbstractApiTest
    {
        [Test]
        public void FindNumbersLocal()
        {
            var expectedJson = GetJsonPayload("/numbers/numbersApi/response/findNumbersLocal.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindNumbersLocalRequest
            {
                Limit = 1,
                Offset = 2,
                Zipcode = "1234",
                State = "LA"
            };
            var numbers = Client.NumbersApi.FindNumbersLocal(request);
            Assert.That(Serializer.Serialize(new ListHolder<Number>(numbers)), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("2")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("zipcode") && p.Value.Equals("1234")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("state") && p.Value.Equals("LA")));
        }

        [Test]
        public void FindNumberRegions()
        {
            var expectedJson = GetJsonPayload("/numbers/numbersApi/response/findNumberRegions.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindNumberRegionsRequest
            {
                Limit = 1,
                Offset = 2,
                Zipcode = "1234",
                State = "LA"
            };
            var regions = Client.NumbersApi.FindNumberRegions(request);
            Assert.That(Serializer.Serialize(regions), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("2")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("zipcode") && p.Value.Equals("1234")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("state") && p.Value.Equals("LA")));
        }

        [Test]
        public void FindNumbersTollfree()
        {
            var expectedJson = GetJsonPayload("/numbers/numbersApi/response/findNumbersTollfree.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new CommonFindRequest
            {
                Limit = 1,
                Offset = 2
            };
            var numbers = Client.NumbersApi.FindNumbersTollfree(request);
            Assert.That(Serializer.Serialize(new ListHolder<Number>(numbers)), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("2")));
        }

    }
}

