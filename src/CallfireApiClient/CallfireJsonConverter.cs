using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System.Globalization;
using RestSharp.Serialization;

namespace CallfireApiClient
{
    /// <summary>
    /// Default JSON serializer for request bodies
    /// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
    /// </summary>
    public class CallfireJsonConverter : IRestSerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer Serializer;

        /// <summary>
        /// Default serializer/deserializer
        /// </summary>
        public CallfireJsonConverter()
        {
            DataFormat = DataFormat.Json;
            Serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new CallfireContractResolver(),
            };
            Serializer.Converters.Add(new StringEnumConverter());
            Serializer.Converters.Add(new UnixTimeJsonConverter());
        }

        /// <summary>
        /// Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.QuoteChar = '"';
                    Serializer.Serialize(jsonTextWriter, obj);
                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        public string Serialize(Parameter parameter)
        {
            return JsonConvert.SerializeObject(parameter.Value);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            using (var stringReader = new StringReader(response.Content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    T result = Serializer.Deserialize<T>(jsonTextReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }

        public DataFormat DataFormat { get; set; }

        public CultureInfo Culture { get; set; }

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };
    }
}
