using System;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Numbers.Model.Request;
using CallfireApiClient.Api.Numbers.Model;
using System.Collections.Generic;

namespace CallfireApiClient.Tests.Api.Numbers
{
    [TestFixture]
    public class NumberLeasesApiTest : AbstractApiTest
    {
        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/response/findNumberLeases.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindNumberLeasesRequest
            {
                Limit = 5,
                Offset = 0,
                State = "LA",
                LabelName = "label"
            };
            var leases = Client.NumberLeasesApi.Find(request);
            Assert.That(Serializer.Serialize(leases), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("state") && p.Value.Equals("LA")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("labelName") && p.Value.Equals("label")));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/response/getNumberLease.json");
            var restRequest = MockRestResponse(expectedJson);

            var lease = Client.NumberLeasesApi.Get("12345678901", FIELDS);
            Assert.That(Serializer.Serialize(lease), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));

            Client.NumberLeasesApi.Get("12345678901");
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields")));   
        }


        [Test]
        public void Update()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/request/updateNumberLease.json");
            var restRequest = MockRestResponse(expectedJson);

            var lease = new NumberLease
            {
                PhoneNumber = "12345678901",
                AutoRenew = false
            };
            Client.NumberLeasesApi.Update(lease);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(expectedJson));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/numbers/leases/12345678901"));
        }

        [Test]
        public void FindConfigs()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/response/findNumberLeaseConfigs.json");
            var restRequest = MockRestResponse(expectedJson);

            var request = new FindNumberLeaseConfigsRequest
            {
                Limit = 5,
                Offset = 0,
                State = "LA",
                LabelName = "label"
            };
            var configs = Client.NumberLeasesApi.FindConfigs(request);
            Assert.That(Serializer.Serialize(configs), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("state") && p.Value.Equals("LA")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("labelName") && p.Value.Equals("label")));
        }

        [Test]
        public void GetConfig()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/response/getNumberLeaseConfig.json");
            var restRequest = MockRestResponse(expectedJson);

            var config = Client.NumberLeasesApi.GetConfig("12345678901", FIELDS);
            Assert.That(Serializer.Serialize(config), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));

            Client.NumberLeasesApi.GetConfig("12345678901");
            Assert.That(restRequest.Value.Parameters, Has.No.Some.Matches<Parameter>(p => p.Name.Equals("fields")));   
        }

        [Test]
        public void UpdateConfig()
        {
            var expectedJson = GetJsonPayload("/numbers/numberLeasesApi/request/updateNumberLeaseConfig.json");
            var restRequest = MockRestResponse(expectedJson);

            var config = new NumberConfig
            {
                Number = "12345678901",
                ConfigType = NumberConfig.NumberConfigType.TRACKING,
                CallTrackingConfig = new CallTrackingConfig
                {
                    Screen = false,
                    Recorded = true,
                    TransferNumbers = new List<string>{ "12135551122", "12135551189" }
                }
            };
            Client.NumberLeasesApi.UpdateConfig(config);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(expectedJson));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/numbers/leases/configs/12345678901"));
        }
    }
}

