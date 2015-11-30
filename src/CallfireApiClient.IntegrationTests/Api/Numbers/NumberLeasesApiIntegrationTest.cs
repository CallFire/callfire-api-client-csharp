using System;
using NUnit.Framework;
using CallfireApiClient.Api.Numbers.Model;
using CallfireApiClient.Api.Numbers.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Numbers
{
    [TestFixture, Ignore("temporary disabled")]
    public class NumberLeasesApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindNumberLeases()
        {
            var request = new FindNumberLeasesRequest { Limit = 2 };
            var leases = Client.NumberLeasesApi.Find(request);
            Console.WriteLine(leases);

            Assert.AreEqual(1, leases.Items.Count);
        }

        [Test]
        public void GetNumberLease()
        {
            const String number = "18728100635";
            var lease = Client.NumberLeasesApi.Get(number);
            Console.WriteLine(lease);

            Assert.IsNotNull(lease.Region);
            Assert.AreEqual(number, lease.PhoneNumber);
            Assert.That(lease.Region.City, Is.StringContaining("APPLETON"));
        }

        [Test]
        public void UpdateNumberLease()
        {
            const string number = "18728100635";
            var lease = Client.NumberLeasesApi.Get(number);
            Assert.IsNotNull(lease.Region);
            var autoRenewSaved = lease.AutoRenew;
            lease.AutoRenew = !autoRenewSaved;
            lease.PhoneNumber = number;

            Client.NumberLeasesApi.Update(lease);
            lease = Client.NumberLeasesApi.Get(number, "autoRenew,tollFree");
            Console.WriteLine(lease);

            Assert.IsNull(lease.PhoneNumber);
            Assert.AreNotEqual(autoRenewSaved, lease.AutoRenew);
        }

        [Test]
        public void FindNumberLeaseConfigs()
        {
            var request = new FindNumberLeaseConfigsRequest { Limit = 2 };
            var configs = Client.NumberLeasesApi.FindConfigs(request);
            Console.WriteLine(configs);

            Assert.AreEqual(1, configs.Items.Count);
        }

        [Test]
        public void GetNumberLeaseConfig()
        {
            var config = Client.NumberLeasesApi.GetConfig("18728100635");
            Console.WriteLine(config);

            Assert.AreEqual(NumberConfig.NumberConfigType.IVR, config.ConfigType);
            Assert.IsNotNull(config.IvrInboundConfig);
        }

        [Test]
        public void UpdateNumberLeaseConfig()
        {
            const string number = "18728100635";
            var config = Client.NumberLeasesApi.GetConfig(number);
            Assert.IsNull(config.CallTrackingConfig);
            Assert.AreEqual(NumberConfig.NumberConfigType.IVR, config.ConfigType);

            Client.NumberLeasesApi.UpdateConfig(config);
            config = Client.NumberLeasesApi.GetConfig(number, "ivrInboundConfig,configType");
            Console.WriteLine(config);

            Assert.IsNotNull(config.IvrInboundConfig);
            Assert.IsNull(config.Number);
            Assert.AreEqual(NumberConfig.NumberConfigType.IVR, config.ConfigType);
            Assert.IsNotNull(config.IvrInboundConfig.DialplanXml);
        }
    }
}

