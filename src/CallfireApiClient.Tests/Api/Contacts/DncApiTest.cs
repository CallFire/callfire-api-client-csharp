using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using RestSharp;
using System;
using System.Linq; 

namespace CallfireApiClient.Tests.Api.Contacts
{
    [TestFixture]
    public class DncApiTest : AbstractApiTest
    {
        //TODO vmalinovskiy: uncomment when dnc apis will be tested and available on docs site
        /*
        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findDncs.json");
            var restRequest = MockRestResponse(expectedJson);

            FindDncNumbersRequest request = new FindDncNumbersRequest
            {
                Limit = 1,
                Offset = 5,
                Fields = FIELDS,
                Prefix = "1",
                Call = true,
                Text = true,
                Numbers = new List<string> { "12135551189" }
            };
 
            var dncs = Client.DncApi.Find(request);
            
            Assert.NotNull(dncs);
            Assert.That(Serializer.Serialize(dncs), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);

            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("prefix") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("call") && p.Value.Equals("True")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("text") && p.Value.Equals("True")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("numbers") && p.Value.Equals("12135551189")));
        }

        [Test]
        public void GetByNullNumberParam()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Client.DncApi.Get(null));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
            Assert.That(ex.Message, Is.StringContaining("Parameter name: number"));
        }

        [Test]
        public void Get()
        {
            var expectedJson = GetJsonPayload("/contacts/dncApi/response/getDnc.json");
            var restRequest = MockRestResponse(expectedJson);

            var contact = Client.DncApi.Get("12135551188");
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(Serializer.Serialize(contact), Is.EqualTo(expectedJson));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/12135551188"));
        }

        [Test]
        public void UpdateByNullNumberParam()
        {
            UpdateDncRequest updRequest = new UpdateDncRequest()
            {
                Call = true,
                Text = false
            };
            var ex = Assert.Throws<ArgumentNullException>(() => Client.DncApi.Update(updRequest));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
            Assert.That(ex.Message, Is.StringContaining("Parameter name: number"));
        }

        [Test]
        public void Update()
        {
            var requestJson = GetJsonPayload("/contacts/dncApi/request/updateDnc.json");
            var restRequest = MockRestResponse();

            UpdateDncRequest updRequest = new UpdateDncRequest()
            {
                Call = true,
                Text = false,
                Number = "100500"
            };
            Client.DncApi.Update(updRequest);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, requestJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/100500"));
        }

        [Test]
        public void Create()
        {
            var requestJson = GetJsonPayload("/contacts/dncApi/request/addDnc.json");
            var restRequest = MockRestResponse();

            CreateDncsRequest updRequest = new CreateDncsRequest()
            {
                Call = true,
                Text = false,
                Numbers = new List<string> { "12135551188" },
                Source = "testSource"
            };
            Client.DncApi.Create(updRequest);

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, requestJson);
        }

        [Test]
        public void DeleteByNullNumberParam()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Client.DncApi.Delete(null));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
            Assert.That(ex.Message, Is.StringContaining("Parameter name: number"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();
            Client.DncApi.Delete("100500");

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/100500"));
        }

        [Test]
        public void DeleteDncsFromSourceByNullSourceParam()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Client.DncApi.DeleteDncsFromSource(null));
            Assert.That(ex.Message, Is.StringContaining("Value cannot be null"));
            Assert.That(ex.Message, Is.StringContaining("Parameter name: source"));
        }

        [Test]
        public void DeleteDncsFromSource()
        {
            var restRequest = MockRestResponse();
            Client.DncApi.DeleteDncsFromSource("testSource");

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/testSource"));
        }

        [Test]
        public void FindUniversalDncs()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findUniversalDncs.json");
            var restRequest = MockRestResponse(expectedJson);

            FindUniversalDncsRequest request = new FindUniversalDncsRequest
            {
                ToNumber = "12135551188",
                FromNumber = "18442800143",
                Fields = FIELDS
            };

            var dncs = Client.DncApi.FindUniversalDncs(request);

            Assert.NotNull(dncs);
            Assert.That(Serializer.Serialize(new ListHolder<UniversalDnc>(dncs)), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/12135551188"));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fromNumber") && p.Value.Equals("18442800143")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
         }
         */
    }
}