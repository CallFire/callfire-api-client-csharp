using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
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
                Number = "12132041238",
                Message = "test message",
                Match = "test match"
            };
            var resourceId = Client.TextAutoRepliesApi.Create(textAutoReply);
            Assert.NotNull(resourceId.Id);

            var request = new FindTextAutoRepliesRequest { Number = "12132041238" };
            var textAutoReplies = Client.TextAutoRepliesApi.Find(request);
            Console.WriteLine(textAutoReplies);

            Assert.True(textAutoReplies.TotalCount > 0);
            Assert.AreEqual(textAutoReplies.Items.Count, textAutoReplies.TotalCount);
            var savedTextAutoReply = textAutoReplies.Items[textAutoReplies.Items.Count - 1];
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

