using CallfireApiClient.Api.Common.Model;
using System.Text;
using System.Collections.Generic;
using RestSharp;
using System;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;

namespace CallfireApiClient
{
    /// <summary>
    /// Utility class
    /// </summary>
    internal static class ClientUtils
    {
        public static readonly IDictionary<string, object> EMPTY_MAP = new Dictionary<string, object>(0);

        /// <summary>
        /// Convert ICollection<T> to pretty string
        /// </summary>
        /// <returns>string representation of IEnumerable object</returns>
        /// <param name="collection">any collection.</param>
        public static string ToPrettyString<T>(this ICollection<T> collection)
        {
            var builder = new StringBuilder();
            foreach (object o in collection)
            {
                if (o is KeyValuePair<string, object>)
                {
                    var pair = (KeyValuePair<string, object>)o;
                    builder.Append(pair.Key).Append(":");
                    if (pair.Value is ICollection)
                    {
                        foreach (var v in pair.Value as ICollection)
                        {
                            builder.Append(v.ToString()).Append(",");
                        }
                        builder.Remove(builder.Length - 1, 1);
                    }
                    else
                    {
                        builder.Append(pair.Value);
                    }
                }
                else
                {
                    builder.Append(o.ToString());
                }
                builder.Append(", ");
            }
            if (collection.Count > 0)
            {
                builder.Remove(builder.Length - 2, 2);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Replaces the first occurence of given string
        /// </summary>
        /// <returns>updated string</returns>
        /// <param name="text">initial string</param>
        /// <param name="search">substring to replace</param>
        /// <param name="replace">replacement string</param>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// Add query param to name-value query list if it's value not null
        /// </summary>
        /// <param name="name">param name</param>
        /// <param name="value">param value</param>
        /// <returns>NameValueCollection with one item</returns>
        public static IDictionary<string, object> BuildQueryParams(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                return new Dictionary<string, object>(1) { { name, value } };
            }
            else
            {
                return EMPTY_MAP;
            }
        }

        public static void PrintParams(List<Parameter> parameters)
        {
            parameters.ForEach(Console.WriteLine);
        }

        /// <summary>
        /// Method traverses request object using reflection and build NameValueCollection from it
        /// <summary>/
        /// <param name="request">request object
        /// <typeparam name="T">type of request<typeparam>/
        /// <returns>collection of query parameters, empty collection if request is null</returns>
        public static IDictionary<string, object> BuildQueryParams<T>(T request)
            where T : CallfireModel
        {
            if (request == null)
            {
                return EMPTY_MAP;
            }
            var parameters = new Dictionary<string, object>();
            foreach (PropertyInfo pi in request.GetType().GetProperties())
            {
                // TODO vmalinovskiy change JsonIgnoreAttribute to some custom IgnoreAsParameter attribure
                var attr = GetPropertyAttributes(pi);
                if (attr.ContainsKey(typeof(JsonIgnoreAttribute).Name))
                    continue;

                object value = pi.GetValue(request, null);
                if (value != null)
                {
                    if (value is ICollection)
                    {
                        parameters.Add(ToCamelCase(pi.Name), value);
                    }
                    else
                    {
                        parameters.Add(ToCamelCase(pi.Name), value.ToString());
                    }
                }
            }
            return parameters;
        }

        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }
                chars[i] = char.ToLowerInvariant(chars[i]);
            }
            return new string(chars);
        }

        public static void AddQueryParamIfSet<T>(string name, IEnumerable<T> value, IList<KeyValuePair<string, object>> queryParams)
        {
            if (name != null && value != null && queryParams != null)
            {
                foreach (T o in value)
                {
                    queryParams.Add(new KeyValuePair<string, object>(name, o.ToString()));
                }
            }
        }

        public static object GetQueryParamByName(string name, IEnumerable<KeyValuePair<string, object>> queryParams)
        {
            if (name != null && queryParams != null)
            {
                foreach (KeyValuePair<string, object> kvp in queryParams)
                {
                    if (kvp.Key.Equals(name))
                    {
                        return kvp.Value;
                    }
                }
            }
            return null;
        }

        private static Dictionary<string, CustomAttributeData> GetPropertyAttributes(PropertyInfo property)
        {
            Dictionary<string, CustomAttributeData> attribs = new Dictionary<string, CustomAttributeData>();

            foreach (CustomAttributeData attribData in property.GetCustomAttributesData())
            {
                string typeName = attribData.Constructor.DeclaringType.Name;
                attribs[typeName] = attribData;
            }
            return attribs;
        }
    }
}