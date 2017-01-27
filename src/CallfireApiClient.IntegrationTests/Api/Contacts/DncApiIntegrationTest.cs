using System;
using NUnit.Framework;
using CallfireApiClient.Api.Contacts.Model;
using CallfireApiClient.Api.Contacts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;

namespace CallfireApiClient.IntegrationTests.Api.Contacts
{
    [TestFixture]
    public class DncApiIntegrationTest : AbstractIntegrationTest
    {

        [Test]
        public void FindDncs()
        {
            var request = new FindDncNumbersRequest()
            {
                Text = true,
                Limit = 1,
                Numbers = new List<string> { "12135551189" }
            };

            Page<DoNotContact> dncs = Client.DncApi.Find(request);
            Console.WriteLine("Page of credentials:" + dncs);

            Assert.NotNull(dncs);
            Assert.AreEqual(dncs.Items.Count, 1);
        }

        [Test]
        public void CrudAndGetDnc()
        {
            CreateDncsRequest crRequest = new CreateDncsRequest()
            {
                Call = true,
                Text = true,
                Numbers = new List<string> { "12135551188" },
                Source = "testSource"
            };
            Client.DncApi.Create(crRequest);

            DoNotContact dnc = Client.DncApi.Get("12135551188");
            Assert.AreEqual(dnc.Number, "12135551188");
            Assert.AreEqual(dnc.Call, true);
            Assert.AreEqual(dnc.Text, true);

            UpdateDncRequest updRequest = new UpdateDncRequest()
            {
                Call = true,
                Text = false,
                Number = "12135551188"
            };
            Client.DncApi.Update(updRequest);

            dnc = Client.DncApi.Get("12135551188");
            Assert.AreEqual(dnc.Call, true);
            Assert.AreEqual(dnc.Text, false);

            Client.DncApi.Delete("12135551188");

            dnc = Client.DncApi.Get("12135551188");
            Assert.AreEqual(dnc.Call, false);
            Assert.AreEqual(dnc.Text, false);
        }

        [Test]
        public void DeleteDncsFromSource()
        {
            CreateDncsRequest crRequest = new CreateDncsRequest()
            {
                Call = true,
                Text = true,
                Numbers = new List<string> { "12135551189" },
                Source = "testSourceForDeleteDncs"
            };
            Client.DncApi.Create(crRequest);

            FindDncNumbersRequest request = new FindDncNumbersRequest()
            {
                Source = "testSourceForDeleteDncs"
            };
            Page<DoNotContact> dncContacts = Client.DncApi.Find(request);
            Assert.True(dncContacts.Items.Count > 0);

            Client.DncApi.DeleteDncsFromSource("testSourceForDeleteDncs");

            dncContacts = Client.DncApi.Find(request);
            Assert.True(dncContacts.Items.Count == 0);
        }

        [Test]
        public void FindUniversalDncs()
        {
            FindUniversalDncsRequest request = new FindUniversalDncsRequest()
            {
                ToNumber = "12135551188",
                FromNumber = "18442800143"
            };

            var uDncs = Client.DncApi.FindUniversalDncs(request);
            Assert.AreEqual("18442800143", uDncs[0].FromNumber);
            Assert.AreEqual("12135551188", uDncs[0].ToNumber);
            Assert.NotNull(uDncs[0].InboundCall);
            Assert.NotNull(uDncs[0].InboundText);
            Assert.NotNull(uDncs[0].OutboundCall);
            Assert.NotNull(uDncs[0].OutboundText);
        }

    }
}