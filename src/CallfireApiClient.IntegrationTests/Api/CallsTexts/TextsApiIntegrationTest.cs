using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using NUnit.Framework;

namespace CallfireApiClient.IntegrationTests.Api.CallsTexts
{
    [TestFixture]
    public class TextsApiIntegrationTest : AbstractIntegrationTest
    {

        [Test]
        public void FindAndGetParticularTexts()
        {
            FindTextsRequest request = new FindTextsRequest();
            request.States = new List<CallfireApiClient.Api.CallsTexts.Model.Text.StateType> { CallfireApiClient.Api.CallsTexts.Model.Text.StateType.FINISHED, CallfireApiClient.Api.CallsTexts.Model.Text.StateType.READY };
            request.Results = new List<TextRecord.TextResult> { TextRecord.TextResult.SENT, TextRecord.TextResult.RECEIVED };
            request.Limit = 2;

            Page<CallfireApiClient.Api.CallsTexts.Model.Text> texts = Client.TextsApi.Find(request);
            Assert.IsNotEmpty(texts.Items);

            CallfireApiClient.Api.CallsTexts.Model.Text text = Client.TextsApi.Get((long)texts.Items[0].Id, "id,fromNumber,state");

            Assert.NotNull(text.Id);
            Assert.NotNull(text.FromNumber);
            Assert.NotNull(text.State);
            Assert.IsNull(text.ToNumber);
        }

        [Test]
        public void SendText()
        {
            TextRecipient recipient1 = new TextRecipient();
            recipient1.Message = "msg";
            recipient1.PhoneNumber = "12132212384";
            TextRecipient recipient2 = new TextRecipient();
            recipient2.Message = "msg";
            recipient2.PhoneNumber = "12132212384";
            var recipients = new List<TextRecipient> { recipient1, recipient2 };
            IList<CallfireApiClient.Api.CallsTexts.Model.Text> texts = Client.TextsApi.Send(recipients, null, "items(id,fromNumber,state)");
            Console.WriteLine("Texts: " + texts);

            Assert.AreEqual(2, texts.Count);
            Assert.NotNull(texts[0].Id);
            Assert.IsNull(texts[0].CampaignId);
            Assert.IsTrue(CallfireApiClient.Api.CallsTexts.Model.Text.StateType.READY == texts[0].State || CallfireApiClient.Api.CallsTexts.Model.Text.StateType.FINISHED == texts[0].State);
        }
    }
}

