using System;
using NUnit.Framework;
using CallfireApiClient.Api.Campaigns.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.IO;
using System.Text;

namespace CallfireApiClient.IntegrationTests.Api.Campaigns
{
    [TestFixture, Ignore("temporary disabled")]
    public class CampaignSoundsApiIntegrationTest : AbstractIntegrationTest
    {
        [Test]
        public void TestFind()
        {
            FindSoundsRequest request = new FindSoundsRequest();
            request.Limit = 3;
            request.Filter = "sample";

            Page<CampaignSound> campaignSounds = Client.CampaignSoundsApi.Find(request);

            Assert.AreEqual(4, campaignSounds.TotalCount);
            Assert.AreEqual(3, campaignSounds.Items.Count);

            foreach (var item in campaignSounds.Items)
            {
                Assert.That(item.Name.Contains("Sample"));
            }
        }

        [Test, Ignore("performs real call to specified number")]
        public void TestCallInToRecord()
        {
            CallCreateSound callCreateSound = new CallCreateSound();
            callCreateSound.Name = "call_in_sound_" + new DateTime().Millisecond;
            callCreateSound.ToNumber = "12132212384";

            ResourceId resourceId = Client.CampaignSoundsApi.RecordViaPhone(callCreateSound);

            Assert.NotNull(resourceId.Id);
        }

        [Test]
        public void TestUploadMp3WavFilesAndGetThem()
        {
            String soundName = "mp3_test_" + DateTime.Now.ToString();

            string mp3FilePath = "Resources/File-examples/train.mp3";
            string wavFilePath = "Resources/File-examples/train.wav";
            ResourceId mp3ResourceId = Client.CampaignSoundsApi.Upload(mp3FilePath, soundName);
            ResourceId wavResourceId = Client.CampaignSoundsApi.Upload(wavFilePath);

            Assert.NotNull(mp3ResourceId.Id);
            Assert.NotNull(wavResourceId.Id);

            // get sound metadata
            CampaignSound campaignSound = Client.CampaignSoundsApi.Get(mp3ResourceId.Id, "name,status,lengthInSeconds");
            Assert.Null(campaignSound.Id);
            Assert.AreEqual(campaignSound.Name, soundName);
            Assert.AreEqual(CampaignSound.Status.ACTIVE, campaignSound.StatusString);
            Assert.AreEqual(6, campaignSound.LengthInSeconds);
            
            // get mp3
            byte[] ms = Client.CampaignSoundsApi.GetMp3(mp3ResourceId.Id);
            string existingFilePath = Path.GetFullPath("Resources/File-examples/train.mp3");
            string pathToSaveNewFile = existingFilePath.Replace("train.mp3", "mp3_sound.mp3");
            File.WriteAllBytes(pathToSaveNewFile, ms);

            // get wav
            ms = Client.CampaignSoundsApi.GetWav(wavResourceId.Id);
            existingFilePath = Path.GetFullPath("Resources/File-examples/train.wav");
            pathToSaveNewFile = existingFilePath.Replace("train.wav", "wav_sound.wav");
            File.WriteAllBytes(pathToSaveNewFile, ms);
        }

        [Test, Ignore("need TTS setup")]
        public void TestCreateFromTts()
        {
            TextToSpeech tts = new TextToSpeech();
            tts.Message = "this is TTS message from csharp client";
            ResourceId resourceId = Client.CampaignSoundsApi.CreateFromTts(tts);
            CampaignSound campaignSound = Client.CampaignSoundsApi.Get(resourceId.Id);
            Assert.AreEqual(resourceId.Id, campaignSound.Id);
            Assert.Greater(campaignSound.LengthInSeconds, 2);
        }
    }
}

