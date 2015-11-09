using System;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;

namespace CallfireApiClient
{
    /// <summary>
    /// Custom ContactResolver is used to populate properties with private modifiers
    /// </summary>
    public class CallfireContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            var pi = member as PropertyInfo;
            if (pi != null)
            {
                prop.Readable = (pi.GetGetMethod(true) != null);
                prop.Writable = (pi.GetSetMethod(true) != null);
            }
            return prop;
        }
    }
}

