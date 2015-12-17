using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using System.Linq;
using CallfireApiClient.IntegrationTests.Api;
using CallfireApiClient.Api.Campaigns;
using CallfireApiClient.Api.Campaigns.Model.Request;

namespace CallfireApiClient.IntegrationTests.Api.Campaigns
{
    [TestFixture, Ignore("temporary disabled")]
    public class TextAutoRepliesApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void CrudOperations()
        {
            var textAutoReply = new TextAutoReply()
            {
                Number = "19206596476",
                Message = "test message",
                Match = "test match"
            };
            var resourceId = Client.TextAutoRepliesApi.Create(textAutoReply);
            Assert.NotNull(resourceId.Id);

            var request = new FindTextAutoRepliesRequest { Number = "19206596476" };
            var textAutoReplies = Client.TextAutoRepliesApi.Find(request);
            Console.WriteLine(textAutoReplies);

            Assert.AreEqual(1, textAutoReplies.TotalCount);
            Assert.AreEqual(1, textAutoReplies.Items.Count);
            var savedTextAutoReply = textAutoReplies.Items[0];
            Assert.AreEqual(resourceId.Id, savedTextAutoReply.Id);
            Assert.AreEqual(textAutoReply.Number, savedTextAutoReply.Number);
            Assert.AreEqual(textAutoReply.Message, savedTextAutoReply.Message);
            Assert.AreEqual(textAutoReply.Match, savedTextAutoReply.Match);

            savedTextAutoReply = Client.TextAutoRepliesApi.Get(resourceId.Id, "number,message");
            Console.WriteLine(savedTextAutoReply);

            Assert.IsNull(savedTextAutoReply.Id);
            Assert.IsNull(savedTextAutoReply.Keyword);
            Assert.AreEqual(textAutoReply.Number, savedTextAutoReply.Number);
            Assert.AreEqual(textAutoReply.Message, savedTextAutoReply.Message);

            Client.TextAutoRepliesApi.Delete(resourceId.Id);

            Assert.Throws<ResourceNotFoundException>(() => Client.TextAutoRepliesApi.Get((long)resourceId.Id));
        }
    }
}

