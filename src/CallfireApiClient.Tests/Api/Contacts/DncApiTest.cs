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
     
        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findDncs.json");
            var restRequest = MockRestResponse(expectedJson);

            FindDncContactsRequest request = new FindDncContactsRequest
            {
                Limit = 1,
                Offset = 5,
                Fields = FIELDS,
                Prefix = "1",
                DncListId = TEST_LONG,
                DncListName = TEST_STRING,
                CallDnc = true,
                TextDnc = true
            };
 
            Page<DoNotContact> dncs = Client.DncApi.Find(request);
            
            Assert.NotNull(dncs);
            Assert.That(Serializer.Serialize(dncs), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
        }

        [Test]
        public void Update()
        {
            string requestJson = GetJsonPayload("/contacts/dncApi/request/updateDnc.json");
            var restRequest = MockRestResponse();

            DoNotContact dnc = new DoNotContact
            {
                Call = true,
                ListId = TEST_LONG,
                Number = TEST_LONG.ToString(),
                Text = true
            };

            Client.DncApi.Update(dnc);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }
    }
}

