﻿using System;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model.Request;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Contacts.Model;

namespace CallfireApiClient.IntegrationTests.Api.Contacts
{
    [TestFixture]
    public class ContactsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void Find()
        {
            var request = new FindContactsRequest{};
            var contacts = Client.ContactsApi.Find(request);
            Console.WriteLine(String.Join(",", contacts));

            Assert.AreEqual(100, contacts.Items.Count);

            string numberToFilter;
            numberToFilter = null;

            if (contacts.Items[0].HomePhone != null)
            {
                numberToFilter = contacts.Items[0].HomePhone;
            }
            else if (contacts.Items[0].WorkPhone != null)
            {
                numberToFilter = contacts.Items[0].WorkPhone;
            }
            else if (contacts.Items[0].MobilePhone != null)
            {
                numberToFilter = contacts.Items[0].MobilePhone;
            }

            if (numberToFilter != null)
            {
                request = new FindContactsRequest
                {
                    Number = new List<string> { numberToFilter }
                };
                contacts = Client.ContactsApi.Find(request);
                Assert.True(numberToFilter.Equals(contacts.Items[0].WorkPhone) ||
                            numberToFilter.Equals(contacts.Items[0].MobilePhone) ||
                            numberToFilter.Equals(contacts.Items[0].HomePhone));
            }
        }

        [Test]
        public void ContactsCRUD()
        {
            var contact1 = new Contact
            {
                HomePhone = "12345678901",
                FirstName = "firstName",
                LastName = "lastName",
                Properties = new Dictionary<string, string> { { "age", "30" }, { "customFieldN", "customValue" } }
            };
            var contact2 = new Contact
            {
                HomePhone = "12345678902"
            };

            var contacts = Client.ContactsApi.Create(new List<Contact> { contact1, contact2 });
            Console.WriteLine(String.Join(",", contacts));

            Assert.AreEqual(2, contacts.Count);

            var savedContact1 = Client.ContactsApi.Get((long)contacts[0].Id);
            Console.WriteLine(savedContact1);
            Assert.AreEqual("12345678901", savedContact1.HomePhone);
            Assert.AreEqual("firstName", savedContact1.FirstName);
            Assert.AreEqual("lastName", savedContact1.LastName);

            contact2.Id = contacts[1].Id;
            contact2.FirstName = "contact2";
            contact2.Zipcode = "12345";
            contact2.Properties = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            Client.ContactsApi.Update(contact2);

            var savedContact2 = Client.ContactsApi.Get((long)contact2.Id, "homePhone,zipcode,properties");
            Console.WriteLine(savedContact2);
            Assert.IsNull(savedContact2.FirstName);
            Assert.AreEqual("12345678902", savedContact2.HomePhone);
            Assert.AreEqual("12345", savedContact2.Zipcode);
            Assert.AreEqual(contact2.Properties, savedContact2.Properties);

            Client.ContactsApi.Delete((long)contacts[0].Id);
            var contact = Client.ContactsApi.Get((long)contacts[0].Id, "id,deleted");
            Assert.IsTrue((bool)contact.Deleted);
            Assert.IsNotNull(contact.Id);
            Assert.IsNull(contact.FirstName);
            Assert.IsNull(contact.HomePhone);
        }

        [Test]
        public void GetContactHistory()
        {
            var request = new GetByIdRequest { Id = 466225637003, Limit = 5 };
            var contactHistory = Client.ContactsApi.GetHistory(request);
            Assert.AreEqual(5, contactHistory.Calls.Count);

            Console.WriteLine("ContactHistory:" + contactHistory);
        }

    }
}

