using System;
using NUnit.Framework;
using CallfireApiClient.Api.Numbers.Model;
using CallfireApiClient.Api.Numbers.Model.Request;

namespace CallfireApiClient.Tests.Integration.Numbers
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

            Assert.True(leases.Items.Count > 0);
        }

        [Test]
        public void GetNumberLease()
        {
            const String number = "12132041238";
            var lease = Client.NumberLeasesApi.Get(number);
            Console.WriteLine(lease);

            Assert.IsNotNull(lease.Region);
            Assert.AreEqual(number, lease.PhoneNumber);
            Assert.That(lease.Region.City, Is.StringContaining("LOS ANGELES"));
        }

        [Test]
        public void UpdateNumberLease()
        {
            const string number = "12132041238";
            var lease = Client.NumberLeasesApi.Get(number);
            Assert.IsNotNull(lease.Region);
            lease.PhoneNumber = number;
            lease.TextFeatureStatus = NumberLease.FeatureStatus.DISABLED;
            lease.CallFeatureStatus = NumberLease.FeatureStatus.DISABLED;

            Client.NumberLeasesApi.Update(lease);
            lease = Client.NumberLeasesApi.Get(number, "number,callFeatureStatus,textFeatureStatus");
            Console.WriteLine(lease);
            Assert.NotNull(lease.PhoneNumber);
            Assert.AreEqual(NumberLease.FeatureStatus.DISABLED, lease.TextFeatureStatus);
            Assert.AreEqual(NumberLease.FeatureStatus.DISABLED, lease.CallFeatureStatus);

            lease.TextFeatureStatus = NumberLease.FeatureStatus.ENABLED;
            lease.CallFeatureStatus = NumberLease.FeatureStatus.ENABLED;
            Client.NumberLeasesApi.Update(lease);
        }

        [Test]
        public void FindNumberLeaseConfigs()
        {
            var request = new FindNumberLeaseConfigsRequest { Limit = 2 };
            var configs = Client.NumberLeasesApi.FindConfigs(request);
            Console.WriteLine(configs);

            Assert.True(configs.Items.Count > 0);
        }

        [Test]
        public void GetNumberLeaseConfig()
        {
            var config = Client.NumberLeasesApi.GetConfig("12132041238");
            Console.WriteLine(config);

            Assert.True(NumberConfig.NumberConfigType.TRACKING.Equals(config.ConfigType));
            Assert.True(config.CallTrackingConfig != null);
        }

        [Test]
        public void UpdateNumberLeaseConfig()
        {
            const string number = "12132041238";
            var config = Client.NumberLeasesApi.GetConfig(number);
            Assert.IsNull(config.IvrInboundConfig);
            Assert.AreEqual(NumberConfig.NumberConfigType.TRACKING, config.ConfigType);

            Client.NumberLeasesApi.UpdateConfig(config);
            config = Client.NumberLeasesApi.GetConfig(number, "callTrackingConfig,configType");
            Console.WriteLine(config);

            Assert.IsNotNull(config.CallTrackingConfig);
            Assert.IsNull(config.Number);
            Assert.AreEqual(NumberConfig.NumberConfigType.TRACKING, config.ConfigType);
        }
    }
}

