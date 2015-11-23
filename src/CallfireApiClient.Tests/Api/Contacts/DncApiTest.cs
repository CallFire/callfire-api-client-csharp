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
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findDncList.json");
            var restRequest = MockRestResponse(expectedJson);

            FindDncContactsRequest request = new FindDncContactsRequest();
            request.Limit = 1;
            request.Offset = 5;
            request.Fields = FIELDS;
            request.prefix = "1";
            request.dncListId = TEST_LONG;
            request.dncListName = TEST_STRING;
            request.callDnc = true;
            request.textDnc = true;
 
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

            DoNotContact dnc = new DoNotContact();
            dnc.call = true;
            dnc.listId = TEST_LONG;
            dnc.number = TEST_LONG.ToString();
            dnc.text = true;
           
            Client.DncApi.Update(dnc);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }



    }
}

