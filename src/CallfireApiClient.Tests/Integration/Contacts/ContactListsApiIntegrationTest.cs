using System;
using System.IO;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Tests.Integration.Contacts
{
    [TestFixture, Ignore("temporary disabled")]
    public class ContactListsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void TestFindContactLists()
        {
            FindContactListsRequest request = new FindContactListsRequest();
            Page<ContactList> contactLists = Client.ContactListsApi.Find(request);
            Console.WriteLine("Page of ContactList:" + contactLists);
            Assert.NotNull(contactLists);
        }

        [Test]
        public void TestCreateContactListFromFile()
        {
            string path = "Resources/File-examples/contacts1.csv";
            ResourceId listId = Client.ContactListsApi.CreateFromCsv("fileList", Path.GetFullPath(path));

            ContactList contactList = Client.ContactListsApi.Get(listId.Id);
            Assert.AreEqual(2, contactList.Size);
            Assert.AreEqual("fileList", contactList.Name);
        }

        [Test]
        public void TestContactListCRUD()
        {
            // create from numbers
            CreateContactListRequest<string> request = new CreateContactListRequest<string>
            {
                Contacts = new List<string> { "12135543211", "12135678882" },
                Name = "listFromNumbers"
            };
            ResourceId numbersListId = Client.ContactListsApi.Create(request);

            ContactList contactList = Client.ContactListsApi.Get(numbersListId.Id);
            Assert.AreEqual(2, contactList.Size);
            Assert.AreEqual(contactList.Name, "listFromNumbers");

            // get items
            GetByIdRequest getItemsRequest = new GetByIdRequest	{ Id = contactList.Id };
            Page<Contact> contactListItems = Client.ContactListsApi.GetListItems(getItemsRequest);
            IList<Contact> items = contactListItems.Items;
            Assert.AreEqual(2, items.Count);

            // create from ids
            var request2 = new CreateContactListRequest<long>
            {
                Contacts = new List<long> { (long)items[0].Id, (long)items[1].Id },
                Name = "listFromExistingContacts"
            };
            ResourceId idsListId = Client.ContactListsApi.Create(request2);

            contactList = Client.ContactListsApi.Get(idsListId.Id);
            Assert.AreEqual(2, contactList.Size);
            Assert.AreEqual(contactList.Name, "listFromExistingContacts");

            // find by name
            FindContactListsRequest findRequest = new FindContactListsRequest { Name = "listFrom" };
            Page<ContactList> contactLists = Client.ContactListsApi.Find(findRequest);
            Assert.Greater(contactLists.TotalCount, 1);

            // update
            UpdateContactListRequest updateListRequest = new UpdateContactListRequest
            { 
                Id = idsListId.Id,  
                Name = "new_name"
            };
            Client.ContactListsApi.Update(updateListRequest);
            ContactList updatedList = Client.ContactListsApi.Get((long)updateListRequest.Id);
            Assert.AreEqual("new_name", updatedList.Name);

            // delete
            Client.ContactListsApi.Delete((long)numbersListId.Id);
            Client.ContactListsApi.Delete((long)idsListId.Id);
        }

        [Test]
        public void TestContactListItemsCRUD()
        {
            Contact c1 = new Contact { FirstName = "name1", HomePhone = "12345678901" };
            Contact c2 = new Contact { FirstName = "name2", HomePhone = "12345678902" };
            CreateContactListRequest<Contact> request = new CreateContactListRequest<Contact>
            {
                Contacts = new List<Contact> { c1, c2 },
                Name = "listFromContacts"
            };
            ResourceId id = Client.ContactListsApi.Create(request);

            AddContactListContactsRequest<string> addItemsRequest = new AddContactListContactsRequest<string>
            {
                ContactListId = id.Id,
                Contacts = new List<string> { "12345543211" }
            };
            Client.ContactListsApi.AddListItems(addItemsRequest);

            GetByIdRequest getItemsRequest = new GetByIdRequest { Id = id.Id };
            Page<Contact> contactListItems = Client.ContactListsApi.GetListItems(getItemsRequest);
            IList<Contact> items = contactListItems.Items;
            Assert.AreEqual(3, items.Count);

            Client.ContactListsApi.RemoveListItem(id.Id, (long)items[0].Id);
            contactListItems = Client.ContactListsApi.GetListItems(getItemsRequest);
            items = contactListItems.Items;
            Assert.AreEqual(2, items.Count);

            Client.ContactListsApi.RemoveListItems(id.Id, new List<long> { (long)items[0].Id, (long)items[1].Id });
            contactListItems = Client.ContactListsApi.GetListItems(getItemsRequest);
            Assert.IsNull(contactListItems.Items);
            Assert.AreEqual(0, contactListItems.TotalCount);

            Client.ContactListsApi.Delete(id.Id);
        }
    }
}

