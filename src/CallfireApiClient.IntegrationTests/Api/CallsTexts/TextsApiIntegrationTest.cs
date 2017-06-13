using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using NUnit.Framework;
using CallfireApiClient.Api.Common.Model.Request;


namespace CallfireApiClient.IntegrationTests.Api.CallsTexts
{
    [TestFixture]
    public class TextsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void FindAndGetParticularTexts()
        {
            var request = new FindTextsRequest
            {
                States = new List<StateType> { StateType.FINISHED, StateType.READY },
                Results = new List<TextRecord.TextResult> { TextRecord.TextResult.SENT, TextRecord.TextResult.RECEIVED },
                Limit = 2
            };

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
            var recipient1 = new TextRecipient { Message = "msg", PhoneNumber = "12132212384" };
            var recipient2 = new TextRecipient { Message = "msg", PhoneNumber = "12132212384", FromNumber = "12132041238" };
            var recipients = new List<TextRecipient> { recipient1, recipient2 };
            
            IList<CallfireApiClient.Api.CallsTexts.Model.Text> texts = Client.TextsApi.Send(recipients, null, "items(id,fromNumber,state)");
            Console.WriteLine("Texts: " + texts);

            Assert.AreEqual(2, texts.Count);
            Assert.NotNull(texts[0].Id);
            Assert.IsNull(texts[0].CampaignId);
            Assert.IsTrue(StateType.READY == texts[0].State || StateType.FINISHED == texts[0].State);

            recipient1.Message = null;
            var request = new SendTextsRequest
            {
                Recipients = recipients,
                CampaignId = 7415135003,
                DefaultMessage = "DefaultLiveMessage",
                Fields = "items(id,fromNumber,state)",
                StrictValidation = true
            };
            texts = Client.TextsApi.Send(request);
            CallfireApiClient.Api.CallsTexts.Model.Text text = Client.TextsApi.Get((long)texts[0].Id);
            Assert.AreEqual(text.Message, "DefaultLiveMessage");
        }
    }
}

