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
    public class DncListsApiTest : AbstractApiTest
    {
        private const string EMPTY_TO_NUMBER_MSG = "toNumber cannot be blank";
        private const string EMPTY_NUMBER_MSG = "number cannot be blank";
        private const string EMPTY_REQ_CONTACT_LIST_ID_MSG = "request.contactListId cannot be null";

        [Test]
        public void Find()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findDncLists.json");
            var restRequest = MockRestResponse(expectedJson);

            FindDncListsRequest request = new FindDncListsRequest();
            request.Limit = 1;
            request.Offset = 5;
            request.Fields = FIELDS; 
            request.CampaignId = TEST_LONG;
            request.Name = TEST_STRING;

            Page<DncList> dncList = Client.DncListsApi.Find(request);

            Assert.NotNull(dncList);
            Assert.That(Serializer.Serialize(dncList), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("1")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("campaignId") && p.Value.Equals(TEST_LONG.ToString())));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("name") && p.Value.Equals(TEST_STRING)));
        }

        [Test]
        public void Create()
        {
            string requestJson = GetJsonPayload("/contacts/dncApi/request/createDncList.json");
            string responseJson = GetJsonPayload("/contacts/dncApi/response/createDncList.json");
            var restRequest = MockRestResponse(responseJson);

            DncList dncList = new DncList();
            dncList.Id = TEST_LONG;
            dncList.CampaignId = TEST_LONG;
            dncList.Size = unchecked((int)TEST_LONG);
            dncList.Name = TEST_STRING;
            ResourceId res = Client.DncListsApi.Create(dncList);

            Assert.That(Serializer.Serialize(res), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void GetUniversalDncNumbersByNullToNumber()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.DncListsApi.GetUniversalDncNumber(null));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_TO_NUMBER_MSG));
            var ex1 = Assert.Throws<ArgumentException>(() => Client.DncListsApi.GetUniversalDncNumber(""));
            Assert.That(ex1.Message, Is.EqualTo(EMPTY_TO_NUMBER_MSG));
        }

        [Test]
        public void GetUniversalDncNumbers()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/getUniversalDncNumbers.json");
            var restRequest = MockRestResponse(expectedJson);

            IList<UniversalDnc> universalDncNumbers = Client.DncListsApi.GetUniversalDncNumber(TEST_LONG.ToString(), TEST_LONG.ToString(), FIELDS);
            
            Assert.NotNull(universalDncNumbers);
            Assert.That(Serializer.Serialize(new ListHolder<UniversalDnc>(universalDncNumbers)), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fromNumber") && p.Value.Equals(TEST_LONG.ToString())));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void GetDnc()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/getDncList.json");
            var restRequest = MockRestResponse(expectedJson);

            DncList dncList = Client.DncListsApi.Get(TEST_LONG, FIELDS);

            Assert.NotNull(dncList);
            Assert.That(Serializer.Serialize(dncList), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }


        [Test]
        public void DeleteDncById()
        {
            var restRequest = MockRestResponse();

            Client.DncListsApi.Delete(TEST_LONG);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
        }

        [Test]
        public void GetDncItemsByNullId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.DncListsApi.GetListItems(new GetByIdRequest()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_REQUEST_ID_MSG));
        }

        [Test]
        public void GetDncItems()
        {
            string expectedJson = GetJsonPayload("/contacts/dncApi/response/findDncList.json");
            var restRequest = MockRestResponse(expectedJson);

            GetByIdRequest request = new GetByIdRequest();
            request.Limit = 1;
            request.Offset = 5;
            request.Fields = FIELDS;
            request.Id = TEST_LONG;

            Page<DoNotContact> dncs = Client.DncListsApi.GetListItems(request);

            Assert.NotNull(dncs);
            Assert.That(Serializer.Serialize(dncs), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void AddDncItemsByNullId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.DncListsApi.AddListItems(new AddDncListItemsRequest<DoNotContact>()));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_REQ_CONTACT_LIST_ID_MSG));
        }

        [Test]
        public void AddDncItems()
        {
            string requestJson = GetJsonPayload("/contacts/dncApi/request/addDncItems.json");
            var restRequest = MockRestResponse();

<<<<<<< HEAD
            List<DoNotContact> dncs = new List<DoNotContact>();
            DoNotContact dnc = new DoNotContact();
            dnc.ListId = TEST_LONG;
            dnc.Number = TEST_LONG.ToString();
=======
            var dncs = new List<DoNotContact>();
            var dnc = new DoNotContact { ListId = TEST_LONG, Number = TEST_LONG.ToString() };
>>>>>>> a38ac4e... added TextAutoRepliesApi and fixed segfault in AddContactsRequest class
            dncs.Add(dnc);
            var request = new AddDncListItemsRequest<DoNotContact>();
            request.ContactListId = TEST_LONG;
            request.Contacts = new List<DoNotContact>();
            request.Contacts.Add(dnc);

            Client.DncListsApi.AddListItems(request);

            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void RemoveListItemByNullNumber()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.DncListsApi.RemoveListItem(TEST_LONG, null));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_NUMBER_MSG));
            var ex1 = Assert.Throws<ArgumentException>(() => Client.DncListsApi.RemoveListItem(TEST_LONG, ""));
            Assert.That(ex1.Message, Is.EqualTo(EMPTY_NUMBER_MSG));
        }


        [Test]
        public void RemoveListItem()
        {
            var restRequest = MockRestResponse();

            Client.DncListsApi.RemoveListItem(TEST_LONG, "123456");

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/123456"));
        }


        [Test]
        public void RemoveListItems()
        {
            var restRequest = MockRestResponse();

            Client.DncListsApi.RemoveListItems(TEST_LONG, new List<string> { "123456", "123457" });

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Is.StringContaining("/" + TEST_LONG));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("number") && p.Value.Equals("123456")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("number") && p.Value.Equals("123457")));
        }

    }
}

