using System;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.IntegrationTests.Api.Contacts
{
    [TestFixture]
    public class DncApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindDncs()
        {
            var request = new FindDncContactsRequest();
            Page<DoNotContact> dncs = Client.DncApi.Find(request);
            Console.WriteLine("Page of credentials:" + dncs);

            Assert.NotNull(dncs);
            Assert.GreaterOrEqual(dncs.Items.Count, 0);
        }

        [Test]
        public void UpdateDnc()
        {
            long listId = 2021478003;
            string number = "13234324554";
            string prefix = "13234";
        
            DoNotContact dncToUpdate = new DoNotContact();
            dncToUpdate.listId = listId;
            dncToUpdate.text = true;
            dncToUpdate.call = true;
            dncToUpdate.number = number;
            Client.DncApi.Update(dncToUpdate);

            var request = new FindDncContactsRequest();
            request.dncListId = listId;
            request.prefix = prefix;
            request.callDnc = true;
            request.textDnc = true;
            request.Limit = 1;
            request.Offset = 0;
            Page<DoNotContact> dnc = Client.DncApi.Find(request);
            Assert.NotNull(dnc);
            Assert.AreEqual(dnc.Items.Count, 1);
            Assert.AreEqual(dnc.Items[0].listId, listId);
            Assert.AreEqual(dnc.Items[0].number, number);
            Assert.AreEqual(dnc.Items[0].text, true);
            Assert.AreEqual(dnc.Items[0].call, true);

            //get back initial db stage as before test
            dncToUpdate.text = true;
            dncToUpdate.call = true;
            Client.DncApi.Update(dncToUpdate);
        }

    }

}

