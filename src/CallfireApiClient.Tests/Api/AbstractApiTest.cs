using System;
using RestSharp.Serializers;
using RestSharp;
using System.Net;
using System.IO;
using System.Text;
using RestSharp.Deserializers;

namespace CallfireApiClient.Tests.Api
{
    public class AbstractApiTest
    {
        protected const long TEST_LONG = 100500;
        protected const string TEST_STRING = "test";
        protected const string FIELDS = "id,name,created";
        protected string ENCODED_FIELDS = "fields=" + WebUtility.UrlEncode(FIELDS);
        protected const string BASE_PATH = "../../JsonMocks";
        protected const string EMPTY_ID_MSG = "id cannot be null";
        protected const string EMPTY_REQUEST_ID_MSG = "request.id cannot be null";
        protected CallfireClient Client;
        protected ISerializer Serializer;
        protected IDeserializer Deserializer;


        public AbstractApiTest()
        {
            Client = new CallfireClient("username", "password");
            Serializer = new CallfireJsonConverter();
            Deserializer = Serializer as IDeserializer;
        }

        protected string GetJsonPayload(string path)
        {
            var result = new StringBuilder(); 
            string[] jsonLines = File.ReadAllLines(BASE_PATH + path);
            foreach (var line in jsonLines)
            {
                string formatted = line.Trim();
                result.Append(formatted.Replace(": ", ":"));
            }
            return result.ToString();
        }

        protected Ref<IRestRequest> MockRestResponse(string responseData = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            byte[] payload = Encoding.ASCII.GetBytes(responseData);
            Client.RestApiClient.RestClient = new MockRestClient(Client.RestApiClient.RestClient, Deserializer,
                new HttpResponse
                {
                    StatusCode = statusCode,
                    RawBytes = payload,
                    ContentLength = payload.Length
                });
            return ((MockRestClient)Client.RestApiClient.RestClient).CapturedRequest;
        }
    }
}

