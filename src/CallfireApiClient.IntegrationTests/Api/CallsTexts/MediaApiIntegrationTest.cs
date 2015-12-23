using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.Common.Model;
using NUnit.Framework;
using System.IO;
using CallfireApiClient;

namespace CallfireApiClient.IntegrationTests.Api.CallsTexts
{
    [TestFixture, Ignore("temporary disabled")]
    public class MediaApiIntegrationTest : AbstractIntegrationTest
    {
        private const string mp3FilePath = "Resources/File-examples/train.mp3";
        private const string wavFilePath = "Resources/File-examples/train.wav";

        [Test, Ignore("Files should not be uploaded before")]
        public void TestUpload()
        {
            String soundName = "mp3_test_" + DateTime.Now.Millisecond.ToString();

            ResourceId wavResourceId = Client.MediaApi.Upload(wavFilePath);
            ResourceId mp3ResourceId = Client.MediaApi.Upload(mp3FilePath, soundName);

            Assert.NotNull(mp3ResourceId.Id);
            Assert.NotNull(wavResourceId.Id);
        }

        [Test]
        public void TestGet()
        {
            String soundName = "mp3_test_" + DateTime.Now.Millisecond.ToString();
            ResourceId mp3ResourceId;
            try
            {
                mp3ResourceId = Client.MediaApi.Upload(mp3FilePath, soundName);
            }
            catch (BadRequestException e)
            {
                mp3ResourceId = new ResourceId { Id = SelectIdFromBadRequestErrorString(e.ApiErrorMessage.Message) };
            }
           
            Media media = Client.MediaApi.Get(mp3ResourceId.Id);

            Assert.NotNull(media);
            Assert.True(media.Name.Contains("mp3_test_"));
            Assert.AreEqual(media.Id, mp3ResourceId.Id);
            Assert.AreEqual(media.MediaType, MediaType.MP3);
            Assert.NotNull(media.LengthInBytes);
            Assert.NotNull(media.Created);
            Assert.NotNull(media.PublicUrl);

            media = Client.MediaApi.Get(mp3ResourceId.Id, "id,created");
            Assert.Null(media.Name);
            Assert.Null(media.LengthInBytes);
            Assert.Null(media.PublicUrl);
            Assert.Null(media.MediaType);
        }

        [Test]
        public void TestGetDataById()
        {
            String soundName = "mp3_test_" + DateTime.Now.Millisecond.ToString();
            ResourceId mp3ResourceId;
            try
            {
                mp3ResourceId = Client.MediaApi.Upload(mp3FilePath, soundName);
            }
            catch (BadRequestException e)
            {
                mp3ResourceId = new ResourceId { Id = SelectIdFromBadRequestErrorString(e.ApiErrorMessage.Message) };
            }

            MemoryStream ms = (MemoryStream)Client.MediaApi.GetData(mp3ResourceId.Id, MediaType.MP3);
            string pathToSaveNewFile = mp3FilePath.Replace("train.mp3", "mp3_sound.mp3");
            File.WriteAllBytes(pathToSaveNewFile, ms.ToArray());
        }

        [Test]
        public void TestGetDataByKey()
        {
            String soundName = "mp3_test_" + DateTime.Now.Millisecond.ToString();
            ResourceId mp3ResourceId;
            try
            {
                mp3ResourceId = Client.MediaApi.Upload(mp3FilePath, soundName);
            }
            catch (BadRequestException e)
            {
                mp3ResourceId = new ResourceId { Id = SelectIdFromBadRequestErrorString(e.ApiErrorMessage.Message) };
            }
            Media media = Client.MediaApi.Get(mp3ResourceId.Id);

            MemoryStream ms = (MemoryStream)Client.MediaApi.GetData(SelectHashPartFromUrlString(media.PublicUrl), MediaType.MP3);
            string pathToSaveNewFile = mp3FilePath.Replace("train.mp3", "mp3_sound.mp3");
            File.WriteAllBytes(pathToSaveNewFile, ms.ToArray());
        }

        private static long SelectIdFromBadRequestErrorString(string message)
        {
            var mediaIdTextStartedAt = message.IndexOf("mediaId:");
            var from = mediaIdTextStartedAt + 9;
            var length = message.Length - from;
            return Int64.Parse(message.Substring(from, length));
        }

        private static string SelectHashPartFromUrlString(string message)
        {
            var hashStartedAt = message.IndexOf("public/") + 7;
            var hashFinishedAt = message.LastIndexOf(".");
            var length = message.Length - hashStartedAt - message.Substring(hashFinishedAt, message.Length - hashFinishedAt).Length;
            var res = message.Substring(hashStartedAt, length);
            return res;
        }
    }
}

