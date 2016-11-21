using System;
using NUnit.Framework;
using CallfireApiClient.Api.Common.Model.Request;
using CallfireApiClient.Api.Account.Model;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Account.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Account
{
    [TestFixture]
    public class MeApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void GetAccount()
        {
            var account = Client.MeApi.GetAccount();
            Console.WriteLine("account: " + account);
        }

        [Test]
        public void GetBillingPlanUsage()
        {
            var plan = Client.MeApi.GetBillingPlanUsage();
            Console.WriteLine("plan: " + plan.ToString());
        }

        [Test]
        public void GetCreditsUsage()
        {
            var request = new DateIntervalRequest
            {
                IntervalBegin = DateTime.UtcNow.AddMonths(-2),
                IntervalEnd = DateTime.UtcNow
            };
            var creditsUsage = Client.MeApi.GetCreditUsage(request);
            Assert.NotNull(creditsUsage.IntervalBegin);
            Assert.NotNull(creditsUsage.IntervalEnd);
            Assert.NotNull(creditsUsage.TextsSent);
            Assert.NotNull(creditsUsage.CallsDurationMinutes);
            Assert.NotNull(creditsUsage.CreditsUsed);

            creditsUsage = Client.MeApi.GetCreditUsage();
            Assert.NotNull(creditsUsage);

            request = new DateIntervalRequest
            {
                IntervalEnd = DateTime.UtcNow.AddMonths(-2)
            };
            creditsUsage = Client.MeApi.GetCreditUsage(request);
            Assert.NotNull(creditsUsage);
        }

        [Test]
        public void GetCallerIds()
        {
            var callerIds = Client.MeApi.GetCallerIds();
            Console.WriteLine("callerIds: " + String.Join(",", callerIds));
        }

        [Test]
        public void SendVerificationCode()
        {
            Client.MeApi.SendVerificationCode("12132212384");
        }

        [Test]
        public void VerifyCallerId()
        {
            var request = new CallerIdVerificationRequest
            {
                CallerId = "12132212384",
                VerificationCode = "123"
            };
            var verified = Client.MeApi.VerifyCallerId(request);
            Console.WriteLine("verified: " + verified);
        }
    }

    [TestFixture, Ignore("require admin credentials to run")]
    public class MeApiCredentialsIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindApiCredentials()
        {
            var request = new CommonFindRequest();
            Page<ApiCredentials> apiCredentials = Client.MeApi.FindApiCredentials(request);
            Console.WriteLine("Page of credentials:" + apiCredentials);
        }

        [Test]
        public void CreateGetDeleteApiCredentials()
        {
            var credentials = new ApiCredentials { Name = "test1" };
            var created = Client.MeApi.CreateApiCredentials(credentials);
            Console.WriteLine(created);

            Assert.AreEqual(credentials.Name, created.Name);
            Assert.IsTrue((bool)created.Enabled);
            Assert.NotNull(created.Id);

            created = Client.MeApi.GetApiCredentials((long)created.Id, "id,name,enabled");
            Assert.AreEqual(credentials.Name, created.Name);
            Assert.IsTrue((bool)created.Enabled);
            Assert.NotNull(created.Id);
            Assert.Null(created.Password);

            Client.MeApi.DeleteApiCredentials((long)created.Id);

            Assert.Throws<ResourceNotFoundException>(() => Client.MeApi.GetApiCredentials((long)created.Id, "name,enabled"));
        }
    }
}

