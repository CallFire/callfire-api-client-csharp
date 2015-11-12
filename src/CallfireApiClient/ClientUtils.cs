using CallfireApiClient.Api.Common.Model;
using System.Text;
using System.Collections.Generic;
using RestSharp;
using System;
using System.Reflection;
using System.Collections;

namespace CallfireApiClient
{
    /// <summary>
    /// Utility class
    /// </summary>
    internal static class ClientUtils
    {
        public static readonly IDictionary<string, object> EMPTY_MAP = new Dictionary<string,object>(0);

        /// <summary>
        /// Convert ICollection<T> to pretty string
        /// </summary>
        /// <returns>string representation of IEnumerable object</returns>
        /// <param name="collection">Enumerable.</param>
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
        public static IDictionary<string,object> BuildQueryParams(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                return new Dictionary<string,object>(1) { { name, value } };
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

        /**
     * Add {@link Iterable} value as query param to name-value query list
     *
     * @param name        parameter name
     * @param value       collection with values
     * @param queryParams parameters list
     */
        //    public static void addQueryParamIfSet(String name, Iterable value, List<NameValuePair> queryParams) {
        //        if (name != null && value != null && queryParams != null) {
        //            for (Object o : value) {
        //                queryParams.add(new BasicNameValuePair(name, o.toString()));
        //            }
        //        }
        //    }

        /// <summary>
        /// Method traverses request object using reflection and build NameValueCollection from it
        /// <summary>/
        /// <param name="request">request object
        /// <typeparam name="T">type of request<typeparam>/
        /// <returns>collection of query parameters, empty collection if request is null</returns>
        public static IDictionary<string,object> BuildQueryParams<T>(T request)
            where T : CallfireModel
        {
            if (request == null)
            {
                return EMPTY_MAP;
            }
            var parameters = new Dictionary<string, object>();
            foreach (PropertyInfo pi in request.GetType().GetProperties())
            {
                object value = pi.GetValue(request, null);
                if (value != null)
                {


                    if (value is ICollection)
                    {
                        parameters.Add(pi.Name.ToLower(), value);
                    }
                    else
                    {
                        parameters.Add(pi.Name.ToLower(), value.ToString());
                    }
                }
            }
            //                TODO vmikhailov remove commented code
            Console.WriteLine("request:" + parameters.ToPrettyString());
            return parameters;
        }
    }
}