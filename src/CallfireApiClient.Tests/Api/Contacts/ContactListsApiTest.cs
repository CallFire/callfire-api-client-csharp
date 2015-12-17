using NUnit.Framework;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using RestSharp;
using System;
using System.Linq;

namespace CallfireApiClient.Tests.Api.Contacts
{
    [TestFixture]
    public class ContactListsApiTest : AbstractApiTest
    {
        private const string RESPONSES_PATH = "/contacts/contactsApi/response/";
        private const string REQUESTS_PATH = "/contacts/contactsApi/request/";
        private const string EMPTY_LIST_ID_MSG = "listId cannot be null";
        private const string EMPTY_CONTACT_ID_MSG = "contactId cannot be null";
        private const string EMPTY_REQ_CONTACT_LIST_ID_MSG = "request.contactListId cannot be null";
        private const string EMPTY_CONTACT_LIST_ID_MSG = "contactListId cannot be null";



        [Test]
        public void TestDynamicPropertiesSerializationStringNumbers()
        {
            CreateContactListRequest<string> requestString = new CreateContactListRequest<string>
            {
                Name = "listFromNumbers",
                Contacts = new List<string> { "12345678881", "12345678882" }
            };

            String serialized = Serializer.Serialize(requestString);
            Assert.That(serialized, Is.StringContaining("\"contactNumbers\":"));
        }

        [Test]
        public void TestDynamicPropertiesSerializationContactIds()
        {
            var requestLong = new CreateContactListRequest<long>
            {
                Name = "listFromIds",
                Contacts = new List<long> { 1, 2 }
            };

            String serialized = Serializer.Serialize(requestLong);
            Assert.That(serialized, Is.StringContaining("\"contactIds\":"));
            Assert.That(serialized, Is.StringContaining("\"listFromIds\""));
        }

        [Test]
        public void TestDynamicPropertiesSerializationContactPojos()
        {
            Contact c1 = new Contact { FirstName = "name1" };
            Contact c2 = new Contact { FirstName = "name1" };

            CreateContactListRequest<Contact> requestContact = new CreateContactListRequest<Contact>();
            requestContact.Name = "listFromContacts";
            requestContact.Contacts = new List<Contact> { c1, c2 };

            String serialized = Serializer.Serialize(requestContact);
            Assert.That(serialized, Is.StringContaining("\"contacts\":"));
            Assert.That(serialized, Is.StringContaining("\"listFromContacts\""));
        }

        [Test]
        public void TestDynamicPropertiesSerializationWithOtherProps()
        {
            AddContactListContactsRequest<long> requestObjects = new AddContactListContactsRequest<long>
            {
                ContactNumbersField = "field",
                ContactListId = 5,
                Contacts = new List<long> { 1, 2 }
            };

            String serialized = Serializer.Serialize(requestObjects);
            Assert.That(serialized, Is.StringContaining("\"contactIds\":"));
            Assert.That(serialized, Is.StringContaining("\"contactNumbersField\":\"field\""));
        }

        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/contacts/contactsApi/response/findContactLists.json");
            var restRequest = MockRestResponse(expectedJson);

            FindContactListsRequest request = new FindContactListsRequest
            {
                Limit = 1,
                Offset = 5,
                Fields = FIELDS,
                Name = TEST_STRING
            };

            Page<ContactList> contactLists = Client.ContactListsApi.Find(request);

            Assert.NotNull(contactLists);
            Assert.That(Serializer.Serialize(contactLists), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("name") && p.Value.Equals(TEST_STRING)));
        }

        [Test]
        public void Create()
        {
            string responseJson = GetJsonPayload("/contacts/contactsApi/response/createContactList.json");
            var restRequest = MockRestResponse(responseJson);

            Contact c1 = new Contact { HomePhone = "123456" };
            Contact c2 = new Contact { HomePhone = "123457" };

            CreateContactListRequest<Contact> requestContact = new CreateContactListRequest<Contact>
            {
                Name = "listFromContacts",
                Contacts = new List<Contact> { c1, c2 }
            };

            ResourceId res = Client.ContactListsApi.Create(requestContact);

            Assert.That(Serializer.Serialize(res), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestCreateFromCsv()
        {
            string responseJson = GetJsonPayload("/contacts/contactsApi/response/createContactList.json");
            var restRequest = MockRestResponse(responseJson);

            ResourceId resourceId = Client.ContactListsApi.CreateFromCsv("fileList", "Resources/File-examples/contacts1.csv");

            Assert.That(Serializer.Serialize(resourceId), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestGetById()
        {
            string expectedJson = GetJsonPayload("/contacts/contactsApi/response/getContactList.json");
            var restRequest = MockRestResponse(expectedJson);

            ContactList contactList = Client.ContactListsApi.Get(TEST_LONG, FIELDS);

            Assert.NotNull(contactList);
            Assert.That(Serializer.Serialize(contactList), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void TestUpdateByNullId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.ContactListsApi.Update(new UpdateContactListRequest()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_REQUEST_ID_MSG));
        }

        [Test]
        public void TestUpdateById()
        {
            string requestJson = GetJsonPayload("/contacts/contactsApi/request/updateContactList.json");
            var restRequest = MockRestResponse();

            UpdateContactListRequest request = new UpdateContactListRequest { Id = TEST_LONG, Name = TEST_STRING };

            Client.ContactListsApi.Update(request);

            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            Assert.AreEqual(Method.PUT, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void TestDelete()
        {
            var restRequest = MockRestResponse();

            Client.ContactListsApi.Delete(TEST_LONG);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
        }

        [Test]
        public void TestGetContactsForContactListByNullId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.ContactListsApi.GetListItems(new GetByIdRequest()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_REQUEST_ID_MSG));
        }

        [Test]
        public void TestGetContactsForContactList()
        {
            string expectedJson = GetJsonPayload("/contacts/contactsApi/response/findContacts.json");
            var restRequest = MockRestResponse(expectedJson);

            GetByIdRequest request = new GetByIdRequest
            {
                Id = TEST_LONG,
                Limit = 1,
                Offset = 5,
                Fields = FIELDS
            };

            Page<Contact> contactsList = Client.ContactListsApi.GetListItems(request);

            Assert.NotNull(contactsList);
            Assert.That(Serializer.Serialize(contactsList), Is.EqualTo(expectedJson));

            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void TestAddContactsToContactListByNullId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.ContactListsApi.AddListItems(new AddContactListContactsRequest<string>()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_REQ_CONTACT_LIST_ID_MSG));
        }

        [Test]
        public void TestAddContactsToContactListById()
        {
            string expectedJson = GetJsonPayload("/contacts/contactsApi/response/addContactsToContactList.json");
            var restRequest = MockRestResponse(expectedJson);

            Contact c1 = new Contact { HomePhone = "123456" };
            Contact c2 = new Contact { HomePhone = "123457" };

            AddContactListContactsRequest<Contact> request = new AddContactListContactsRequest<Contact>
            {
                ContactNumbersField = "homePhone",
                ContactListId = TEST_LONG,
                Contacts = new List<Contact> { c1, c2 }
            };

            Client.ContactListsApi.AddListItems(request);

            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.AreEqual(requestBodyParam.Value, expectedJson);
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
        }

        [Test]
        public void RemoveListItem()
        {
            var restRequest = MockRestResponse();

            Client.ContactListsApi.RemoveListItem(TEST_LONG, 123456);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/123456"));
        }

        [Test]
        public void RemoveListItems()
        {
            var restRequest = MockRestResponse();

            Client.ContactListsApi.RemoveListItems(TEST_LONG, new List<long> { 123456, 123457 });

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("123456")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("id") && p.Value.Equals("123457")));
        }

    }
}

