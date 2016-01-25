using System;
using System.IO;
using RestSharp.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using RestSharp.Deserializers;
using RestSharp;
using System.Globalization;

namespace CallfireApiClient
{
    /// <summary>
    /// Default JSON serializer for request bodies
    /// Doesn't currently use the SerializeAs attribute, defers to Newtonsoft's attributes
    /// </summary>
    public class CallfireJsonConverter : ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer Serializer;

        /// <summary>
        /// Default serializer/deserializer
        /// </summary>
        public CallfireJsonConverter()
        {
            ContentType = "application/json";
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

        public CultureInfo Culture { get; set; }
    }
}
