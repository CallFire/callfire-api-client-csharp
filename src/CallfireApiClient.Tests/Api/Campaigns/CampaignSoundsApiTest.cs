using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Tests.Api.Campaigns
{
    [TestFixture]
    public class CampaignSoundsApiTest : AbstractApiTest
    {
        [Test]
        public void TestFind()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/findCampaignSounds.json");
            var restRequest = MockRestResponse(expectedJson);

            FindSoundsRequest request = new FindSoundsRequest();
            request.Limit = 5;
            request.Offset = 0;
            request.Filter = "1234";

            Page<CampaignSound> sounds = Client.CampaignSoundsApi.Find(request);
 
            Assert.That(Serializer.Serialize(sounds), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("filter") && p.Value.Equals("1234")));
        }

        [Test]
        public void TestGet()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/getCampaignSound.json");
            var restRequest = MockRestResponse(expectedJson);

            CampaignSound campaignSound = Client.CampaignSoundsApi.Get(11, FIELDS);

            Assert.That(Serializer.Serialize(campaignSound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Is.StringEnding("/11"));

            Client.CampaignSoundsApi.Get(11);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("FIELDS") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void TestUpload()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = "Resources/File-examples/train.mp3";
            ResourceId id = Client.CampaignSoundsApi.Upload(mp3FilePath, "fname");

            Assert.That(Serializer.Serialize(id), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestRecordViaPhone()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/recordViaPhone.json");
            var restRequest = MockRestResponse(responseJson);

            CallCreateSound callCreateSound = new CallCreateSound();
            callCreateSound.Name = "My sound file";
            callCreateSound.ToNumber = "12135551122";
            ResourceId id = Client.CampaignSoundsApi.RecordViaPhone(callCreateSound);

            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }

        [Test]
        public void TestCreateFromTts()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/createFromTts.json");
            var restRequest = MockRestResponse(responseJson);

            TextToSpeech textToSpeech = new TextToSpeech();
            textToSpeech.Voice = Voice.MALE1;
            textToSpeech.Message = "This is a TTS sound";
            ResourceId id = Client.CampaignSoundsApi.CreateFromTts(textToSpeech);

            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(requestBodyParam.Value, Is.EqualTo(requestJson));
        }
    }
}

