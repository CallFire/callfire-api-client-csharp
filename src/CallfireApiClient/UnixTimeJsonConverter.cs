using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace CallfireApiClient
{
    /// <summary>
    /// Custom DateTime converter to convert incoming Unix UTC time to C# DateTime
    /// </summary>
    public class UnixTimeJsonConverter: DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? (DateTime?)null : Epoch.AddMilliseconds(Convert.ToInt64(reader.Value)).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long unixTime = (long)(Convert.ToDateTime(value).ToUniversalTime() - Epoch).TotalMilliseconds;
            writer.WriteValue(unixTime);
        }
    }
}

