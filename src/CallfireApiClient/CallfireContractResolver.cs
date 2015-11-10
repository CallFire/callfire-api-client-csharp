using System;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections;

namespace CallfireApiClient
{
    /// <summary>
    /// Custom ContactResolver is used to populate properties with private modifiers, serialize properties to camelCase
    /// names and skip serialization of empty collections
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
            Predicate<object> shouldSerialize = prop.ShouldSerialize;
            prop.ShouldSerialize = obj => (shouldSerialize == null || shouldSerialize(obj)) && !IsEmptyCollection(prop, obj);
            return prop;
        }

        private bool IsEmptyCollection(JsonProperty property, object target)
        {
            var value = property.ValueProvider.GetValue(target);
            var collection = value as ICollection;
            if (collection != null && collection.Count == 0)
            {
                return true;
            }
            if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                return false;
            }
            var countProp = property.PropertyType.GetProperty("Count");
            if (countProp == null)
            {
                return false;
            }

            var count = (int)countProp.GetValue(value, null);
            return count == 0;
        }
    }
}

