using NUnit.Framework;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.CallsTexts.Model;
using RestSharp;
using System;
using System.IO;

namespace CallfireApiClient.Tests.Api.CallsTexts
{
    [TestFixture]
    public class MediaApiTest : AbstractApiTest
    {
        private const string EMPTY_KEY_MSG = "key cannot be blank";
        
        [Test]
        public void Upload()
        {
            string expectedJson = GetJsonPayload("/callstexts/mediaApi/response/uploadSound.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = "Resources/File-examples/train.mp3";
            ResourceId id = Client.MediaApi.Upload(mp3FilePath, "fname");

            Assert.That(Serializer.Serialize(id), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void UploadWithFileData()
        {
            string expectedJson = GetJsonPayload("/callstexts/mediaApi/response/uploadSound.json");
            var restRequest = MockRestResponse(expectedJson);

            string mp3FilePath = "Resources/File-examples/train.mp3";
            ResourceId id = Client.MediaApi.Upload(File.ReadAllBytes(mp3FilePath), MediaType.MP3, "fname");

            Assert.That(Serializer.Serialize(id), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.POST, restRequest.Value.Method);
        }

        [Test]
        public void GetMedia()
        {
            string expectedJson = GetJsonPayload("/callstexts/mediaApi/response/getMedia.json");
            var restRequest = MockRestResponse(expectedJson);

            Media media = Client.MediaApi.Get(1); 
            Assert.That(restRequest.Value.Parameters, Has.None.Matches<Parameter>(p => p.Name.Equals("fields")));

            media = Client.MediaApi.Get(1, FIELDS);
            Assert.That(Serializer.Serialize(media), Is.EqualTo(expectedJson));
            Assert.AreEqual(Method.GET, restRequest.Value.Method);
            Assert.That(restRequest.Value.Parameters, Has.Some.Matches<Parameter>(p => p.Name.Equals("fields") && p.Value.Equals(FIELDS)));
        }

        [Test]
        public void GetDataByBlankKey()
        {
            var ex = Assert.Throws<ArgumentException>(() => Client.MediaApi.GetData(null, MediaType.BMP));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));

            ex = Assert.Throws<ArgumentException>(() => Client.MediaApi.GetData("", MediaType.BMP));
            Assert.That(ex.Message, Is.EqualTo(EMPTY_KEY_MSG));
        }
    }
}

