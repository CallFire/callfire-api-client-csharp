using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using System.Linq;
using CallfireApiClient.IntegrationTests.Api;

namespace CallfireApiClient.IntegrationTests.Api.Campaigns
{
    [TestFixture, Ignore("temporary disabled")]
    public class BatchesApiTest : AbstractIntegrationTest
    {
        [Test]
        public void Get()
        {
            var batch = Client.BatchesApi.Get(5506387003);
            Console.WriteLine("batch: " + batch);
        }

        [Test]
        public void Update()
        {
            var batch = Client.BatchesApi.Get(5506387003);
            Console.WriteLine("batch: " + batch);
            batch.Enabled = false;

            Client.BatchesApi.Update(batch);
            var updated = Client.BatchesApi.Get(5506387003);
            Console.WriteLine("updated batch: " + updated);
        }
    }
}

