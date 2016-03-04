using CallfireApiClient.Api.Common.Model;
using System.Text;
using System.Collections.Generic;
using RestSharp;
using System;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Linq;

namespace CallfireApiClient
{
    /// <summary>
    /// Utility class
    /// </summary>
    internal static class ClientUtils
    {
        public static readonly IList<KeyValuePair<string, object>> EMPTY_MAP = new List<KeyValuePair<string, object>>(0);

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
        public static IList<KeyValuePair<string, object>> BuildQueryParams(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                return new List<KeyValuePair<string, object>>(1) { new KeyValuePair<string, object>(name, value) };
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
        /// <typeparam name="T">type of request</typeparam>
        /// <returns>collection of query parameters, empty collection if request is null</returns>
        public static IList<KeyValuePair<string, object>> BuildQueryParams<T>(T request)
            where T : CallfireModel
        {
            if (request == null)
            {
                return EMPTY_MAP;
            }
            var parameters = new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo pi in request.GetType().GetProperties())
            {
                // TODO vmalinovskiy change JsonIgnoreAttribute to some custom IgnoreAsParameter attribure
                var attr = GetPropertyAttributes(pi);
                if (attr.ContainsKey(typeof(JsonIgnoreAttribute).Name))
                {
                    continue;
                }

                object value = pi.GetValue(request, null);
                AddQueryParamIfSet(pi.Name, value, parameters);
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

        public static void AddQueryParamIfSet(string name, object value, IList<KeyValuePair<string, object>> queryParams)
        {
            if (name != null && value != null && queryParams != null)
            {
                if (value is ICollection)
                {
                    foreach (object o in (ICollection)value)
                    {
                        queryParams.Add(new KeyValuePair<string, object>(ToCamelCase(name), o.ToString()));
                    } 
                }
                else if (value is DateTime)
                {
                    queryParams.Add(new KeyValuePair<string, object>(ToCamelCase(name), ToUnixTime((DateTime)value)));
                }
                else
                {
                    queryParams.Add(new KeyValuePair<string, object>(ToCamelCase(name), value.ToString()));
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

        public static long ToUnixTime(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - ClientConstants.EPOCH).TotalMilliseconds;
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

        /// <summary>
        /// Returns a EnumMember attribute for object
        /// </summary>
        /// <param name="source">object to select EnumMember attr from</param>
        /// <returns>EnumMemberAttribute for input object</returns>
        public static string EnumMemberAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            EnumMemberAttribute[] attributes = (EnumMemberAttribute[])fi.GetCustomAttributes(
                typeof(EnumMemberAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Value;
            else return source.ToString();
        }

        /// <summary>
        /// Returns a EnumMember attribute for object
        /// </summary>
        /// <param name="source">object to select EnumMember attr from</param>
        /// <returns>EnumMemberAttribute for input object</returns>
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        /// <summary>
        /// Returns enum by description attribute
        /// </summary>
        /// <param name="description">enum description attribute value</param>
        /// <returns>enum object</returns>
        public static T EnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
                                (f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        /// <summary>
        /// Returns int converted from string with default possible value
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <param name="def">default to return if s is null</param>
        /// <returns>int value</returns>
        public static int StrToIntDef(string s, int def)
        {
            if (!Int32.TryParse(s, out def))
                return def;

            return Int32.Parse(s);
        }
    }
}