using NUnit.Framework;
using CallfireApiClient.Api.Numbers.Model;
using CallfireApiClient.Api.Numbers.Model.Request;
using System.Collections.Generic;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.IntegrationTests.Api.Numbers
{
    [TestFixture]
    public class NumberLeasesApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindNumberLeases()
        {
            var request = new FindNumberLeasesRequest { Limit = 2 };
            var leases = Client.NumberLeasesApi.Find(request);
            System.Console.WriteLine(leases);

            Assert.True(leases.Items.Count > 0);
            Assert.True(leases.Items[0].Labels.Count > 0);
        }

        [Test]
        public void GetNumberLease()
        {
            const System.String number = "12132041238";
            var lease = Client.NumberLeasesApi.Get(number);
            System.Console.WriteLine(lease);

            Assert.IsNotNull(lease.Region);
            Assert.AreEqual(number, lease.PhoneNumber);
            Assert.True(lease.Labels.Count > 0);
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
            System.Console.WriteLine(lease);
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
            System.Console.WriteLine(configs);

            Assert.True(configs.Items.Count > 0);
        }

        [Test]
        public void GetNumberLeaseConfig()
        {
            var config = Client.NumberLeasesApi.GetConfig("12132041238");
            System.Console.WriteLine(config);

            Assert.True(NumberConfig.NumberConfigType.TRACKING.Equals(config.ConfigType));
            Assert.True(config.CallTrackingConfig != null);
        }

        [Test]
        public void UpdateNumberLeaseConfig()
        {
            const string number = "12132041238";
            var config = Client.NumberLeasesApi.GetConfig(number, "number,configType,callTrackingConfig");
            Assert.IsNull(config.IvrInboundConfig);
            Assert.AreEqual(NumberConfig.NumberConfigType.TRACKING, config.ConfigType);
            CallTrackingConfig callTrackingConfig = new CallTrackingConfig();
            callTrackingConfig.Recorded = true;
            callTrackingConfig.Screen = true;
            callTrackingConfig.TransferNumbers = new List<string> { "12132212384" };
            callTrackingConfig.Voicemail = true;
            callTrackingConfig.IntroSoundId = 1;
            callTrackingConfig.VoicemailSoundId = 1;
            callTrackingConfig.FailedTransferSoundId = 1;
            callTrackingConfig.WhisperSoundId = 1;

            WeeklySchedule weeklySchedule = new WeeklySchedule
            {
                StartTimeOfDay = new LocalTime { Hour = 1, Minute = 1, Second = 1 },
                StopTimeOfDay = new LocalTime { Hour = 2, Minute = 2, Second = 2 },
                TimeZone = "America/New_York",
                DaysOfWeek = new HashSet<DayOfWeek> { DayOfWeek.MONDAY, DayOfWeek.FRIDAY, DayOfWeek.SATURDAY }
            };
            callTrackingConfig.WeeklySchedule = weeklySchedule;

            GoogleAnalytics googleAnalytics = new GoogleAnalytics
            {
                Category = "Sales",
                GoogleAccountId = "UA-12345-26",
                Domain = "testDomain"
            };
            callTrackingConfig.GoogleAnalytics = googleAnalytics;

            config.CallTrackingConfig = callTrackingConfig;
            config.ConfigType = NumberConfig.NumberConfigType.TRACKING;

            Client.NumberLeasesApi.UpdateConfig(config);
            config = Client.NumberLeasesApi.GetConfig(number);
            System.Console.WriteLine(config);

            config = Client.NumberLeasesApi.GetConfig(number, "callTrackingConfig,configType");
            Assert.IsNotNull(config.CallTrackingConfig);
            Assert.IsNull(config.Number);
            Assert.AreEqual(NumberConfig.NumberConfigType.TRACKING, config.ConfigType);
        }
    }
}