using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using RestSharp;
using System.Linq;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.IO;

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

            FindSoundsRequest request = new FindSoundsRequest
            {
                Limit = 5,
                Offset = 0,
                Filter = "1234",
                IncludeArchived = true,
                IncludePending = true,
                IncludeScrubbed = true
            };

            Page<CampaignSound> sounds = Client.CampaignSoundsApi.Find(request);
 
            Assert.That(Serializer.Serialize(sounds), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("limit") && p.Value.Equals("5")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("offset") && p.Value.Equals("0")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("filter") && p.Value.Equals("1234")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("includeArchived") && p.Value.Equals("True")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("includePending") && p.Value.Equals("True")));
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("includeScrubbed") && p.Value.Equals("True")));
        }

        [Test]
        public void TestDelete()
        {
            var restRequest = MockRestResponse();

            Client.CampaignSoundsApi.Delete(11L);

            Assert.AreEqual(Method.DELETE, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11"));
        }

        [Test]
        public void TestGetWithFieldsParam()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/getCampaignSound.json");
            var restRequest = MockRestResponse(expectedJson);

            CampaignSound campaignSound = Client.CampaignSoundsApi.Get(11, FIELDS);

            Assert.That(Serializer.Serialize(campaignSound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11"));
        }

        [Test]
        public void TestGet()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/getCampaignSound.json");
            var restRequest = MockRestResponse(expectedJson);

            CampaignSound campaignSound = Client.CampaignSoundsApi.Get(11);

            Assert.That(Serializer.Serialize(campaignSound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Resource, Does.EndWith("/11"));
        }

        [Test]
        public void TestUpload()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = GetFullPath("/Resources/File-examples/train.mp3");
            ResourceId id = Client.CampaignSoundsApi.Upload(mp3FilePath, "fname");

            Assert.That(Serializer.Serialize(id), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestUploadAndGetSoundDetailsWithoutFileName()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = GetFullPath("/Resources/File-examples/train.mp3");
            CampaignSound sound = Client.CampaignSoundsApi.UploadAndGetSoundDetails(mp3FilePath);
            Assert.That(Serializer.Serialize(sound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestUploadAndGetSoundDetailsWithFileName()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = GetFullPath("/Resources/File-examples/train.mp3");
            CampaignSound sound = Client.CampaignSoundsApi.UploadAndGetSoundDetails(mp3FilePath, "fname");
            Assert.That(Serializer.Serialize(sound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestUploadAndGetSoundDetailsWithFileNameAndFields()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = GetFullPath("/Resources/File-examples/train.mp3");
            CampaignSound sound = Client.CampaignSoundsApi.UploadAndGetSoundDetails(mp3FilePath, "fname", FIELDS);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("FIELDS") && p.Value.Equals(FIELDS)));
            Assert.That(Serializer.Serialize(sound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestUploadWithFileData()
        {
            string expectedJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = GetFullPath("/Resources/File-examples/train.mp3");
            CampaignSound sound = Client.CampaignSoundsApi.UploadAndGetSoundDetails(File.ReadAllBytes(mp3FilePath), "fname", FIELDS);
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("FIELDS") && p.Value.Equals(FIELDS)));
            Assert.That(Serializer.Serialize(sound), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void TestRecordViaPhone()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/recordViaPhone.json");
            var restRequest = MockRestResponse(responseJson);

            CallCreateSound callCreateSound = new CallCreateSound
            {
                Name = "My sound file",
                ToNumber = "12135551122"
            };

            ResourceId id = Client.CampaignSoundsApi.RecordViaPhone(callCreateSound);

            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
        }

        [Test]
        public void TestRecordViaPhoneAndGetSoundDetails()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/recordViaPhone.json");
            var restRequest = MockRestResponse(responseJson);

            CallCreateSound callCreateSound = new CallCreateSound
            {
                Name = "My sound file",
                ToNumber = "12135551122"
            };

            CampaignSound sound = Client.CampaignSoundsApi.RecordViaPhoneAndGetSoundDetails(callCreateSound, FIELDS);

            Assert.That(Serializer.Serialize(sound), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("FIELDS") && p.Value.Equals(FIELDS)));

        }

        [Test]
        public void TestCreateFromTts()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSound.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/createFromTts.json");
            var restRequest = MockRestResponse(responseJson);

            TextToSpeech textToSpeech = new TextToSpeech
            {
                Voice = Voice.MALE1,
                Message = "This is a TTS sound"
            };

            ResourceId id = Client.CampaignSoundsApi.CreateFromTts(textToSpeech);

            Assert.That(Serializer.Serialize(id), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
        }

        [Test]
        public void TestCreateFromTtsAndGetSoundDetails()
        {
            string responseJson = GetJsonPayload("/campaigns/campaignSoundsApi/response/uploadSoundWithDetails.json");
            string requestJson = GetJsonPayload("/campaigns/campaignSoundsApi/request/createFromTts.json");
            var restRequest = MockRestResponse(responseJson);

            TextToSpeech textToSpeech = new TextToSpeech
            {
                Voice = Voice.MALE1,
                Message = "This is a TTS sound"
            };

            CampaignSound sound = Client.CampaignSoundsApi.CreateFromTtsAndGetSoundDetails(textToSpeech, FIELDS);

            Assert.That(Serializer.Serialize(sound), Is.EqualTo(responseJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
            var requestBodyParam = restRequest.Value.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Assert.That(Serializer.Serialize(requestBodyParam.Value), Is.EqualTo(requestJson));
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("FIELDS") && p.Value.Equals(FIELDS)));
        }
    }
}

