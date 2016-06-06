﻿using System;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Contacts
{
    [TestFixture]
    public class DncListsApiIntegrationTest : AbstractIntegrationTest
    {

        [Test]
        public void TestDncContactListWithItemsCRUD()
        {
            //test add and get dnc list
            DncList dncList = new DncList { Name = "dncList1" };
            ResourceId dncListId = Client.DncListsApi.Create(dncList);
            DncList created = Client.DncListsApi.Get(dncListId.Id);
            Assert.AreEqual("dncList1", created.Name);
            Assert.Greater(created.Created, DateTime.Now.AddMinutes(-3));

            //test find dnc list
            FindDncListsRequest findRequest = new FindDncListsRequest { Name = "dncList1" };
            Page<DncList> doNotCallLists = Client.DncListsApi.Find(findRequest);
            Assert.Greater(doNotCallLists.TotalCount, 0);
            Console.WriteLine("Page of dnc list:" + doNotCallLists);

            //test add dnc list items
            DoNotContact dnc1 = new DoNotContact { Number = "12135543211", Text = true, Call = false };
            DoNotContact dnc2 = new DoNotContact { Number = "12135543212", Text = true, Call = false };
            DoNotContact dnc3 = new DoNotContact { Number = "12135543213", Text = true, Call = false };

            AddDncListItemsRequest<DoNotContact> addItemsRequest = new AddDncListItemsRequest<DoNotContact>
            {
                ContactListId = dncListId.Id,
                Contacts = new List<DoNotContact> { dnc1, dnc2, dnc3 }
            };
            Client.DncListsApi.AddListItems(addItemsRequest);

            //test get dnc list items
            GetByIdRequest getItemsRequest = new GetByIdRequest { Id = dncListId.Id };
            Page<DoNotContact> dncListItems = Client.DncListsApi.GetListItems(getItemsRequest);
            IList<DoNotContact> items = dncListItems.Items;
            Assert.AreEqual(3, items.Count);

            //test remove dnc list items
            Client.DncListsApi.RemoveListItem(dncListId.Id, "12135543211");
            dncListItems = Client.DncListsApi.GetListItems(getItemsRequest);
            items = dncListItems.Items;
            Assert.AreEqual(2, items.Count);

            Client.DncListsApi.RemoveListItems(dncListId.Id, new List<string> { "12135543212", "12135543213" });

            dncListItems = Client.DncListsApi.GetListItems(getItemsRequest);
            Assert.AreEqual(null, dncListItems.Items);
            Assert.AreEqual(0, dncListItems.TotalCount);

            // test delete dnc list
            Client.DncListsApi.Delete(dncListId.Id);

            // test get UniversalDncNumber
            IList<UniversalDnc> universalDncNumber = Client.DncListsApi.GetUniversalDncNumber("12135543212");
            Console.WriteLine("universal:" + universalDncNumber);
        }
    }
}

