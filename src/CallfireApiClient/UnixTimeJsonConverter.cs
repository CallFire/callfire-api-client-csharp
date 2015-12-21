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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime local = ClientConstants.EPOCH.AddMilliseconds(Convert.ToInt64(reader.Value)).ToLocalTime();
            return reader.Value == null ? (DateTime?)null : local;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ClientUtils.ToUnixTime(Convert.ToDateTime(value)));
        }
    }
}

