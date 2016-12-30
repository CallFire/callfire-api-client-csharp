using System;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;

namespace CallfireApiClient.Tests.Api.Contacts
{
    [TestFixture]
    public class ContactsApiTest : AbstractApiTest
    {

        [Test]
        public void Find()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/response/findContacts.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new FindContactsRequest
            {
                Number = new List<string> { "123", "456" },
                Id = new List<long> { 124, 457 },
                Limit = 1,
                Offset = 5
            };
            var contacts = Client.ContactsApi.Find(request);
            Assert.That(Serializer.Serialize(contacts), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("number") && p.Value.Equals("123")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("number") && p.Value.Equals("456")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("124")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("457")));
        }

        [Test]
        public void Create()
        {
            var requestJson = GetJsonPayload("/contacts/contactsApi/request/createContact.json");
            var responseJson = GetJsonPayload("/contacts/contactsApi/response/createContact.json");
            var restRequest = MockRestResponse(responseJson);

            var contact = new Contact
            {
                FirstName = "Test",
                LastName = "Contact",
                HomePhone = "12135551124"
            };
            var contact2 = new Contact
            {
                FirstName = "Test2",
                LastName = "Contact2",
                HomePhone = "12135551125"
            };
            var ids = Client.ContactsApi.Create(new List<Contact>{ contact, contact2 });
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(Serializer.Serialize(new ListHolder<ResourceId>(ids)), Is.EqualTo(responseJson));
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void GetById()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/response/getContactById.json");
            var restRequest = MockRestResponse(expectedJson);

            var contact = Client.ContactsApi.Get(11);
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(Serializer.Serialize(contact), Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetByIdAndFields()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/response/getContactById.json");
            var restRequest = MockRestResponse(expectedJson);

            var contact = Client.ContactsApi.Get(11, FIELDS);
            Assert.That(Serializer.Serialize(contact), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void Update()
        {
            var requestJson = GetJsonPayload("/contacts/contactsApi/request/updateContact.json");
            var restRequest = MockRestResponse();
            var contact = new Contact
            {
                Id = 11,
                FirstName = "Test",
                LastName = "Contact",
                WorkPhone = "12135551124"
            };
            Client.ContactsApi.Update(contact);

            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, requestJson);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void Delete()
        {
            var restRequest = MockRestResponse();
            Client.ContactsApi.Delete(11);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));
        }

        [Test]
        public void GetContactHistory()
        {
            var expectedJson = GetJsonPayload("/contacts/contactsApi/response/getContactHistory.json");
            var restRequest = MockRestResponse(expectedJson);
            var request = new GetByIdRequest
            {
                Id = 1,
                Limit = 1,
                Offset = 5
            };
            var contactHistory = Client.ContactsApi.GetHistory(request);
            Assert.That(Serializer.Serialize(contactHistory), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.IsNull(requestBodyParam);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
        }

    }
}