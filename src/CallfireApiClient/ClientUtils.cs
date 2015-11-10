using CallfireApiClient.Api.Common.Model;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using RestSharp;
using System;
using System.Reflection;

namespace CallfireApiClient
{
    /// <summary>
    /// Utility class
    /// </summary>
    public static class ClientUtils
    {
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
        /// Converts NameValueCollection to string
        /// </summary>
        /// <returns>string representation of collection</returns>
        /// <param name="queryParams">collection to convert</param>
        public static string ParamsToString(NameValueCollection queryParams)
        {
            var result = new StringBuilder();
            foreach (string key in queryParams.AllKeys)
            {
                result.AppendFormat("{0} = {1} ", key, string.Join(",", queryParams.GetValues(key)));
            }
            return result.ToString();
        }

        /// <summary>
        /// Add query param to name-value query list if it's value not null
        /// </summary>
        /// <param name="name">param name</param>
        /// <param name="value">param value</param>
        /// <returns>NameValueCollection with one item</returns>
        public static NameValueCollection BuildQueryParams(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                return new NameValueCollection(1) { { name, value } };
            }
            else
            {
                return new NameValueCollection(0);
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
        public static NameValueCollection BuildQueryParams<T>(T request)
            where T : CallfireModel
        {
            if (request == null)
            {
                return new NameValueCollection(0);
            }
            var parameters = new NameValueCollection();
            foreach (PropertyInfo pi in request.GetType().GetProperties())
            {
                object value = pi.GetValue(request, null);
                if (value != null)
                {
                    parameters.Add(pi.Name.ToLower(), value.ToString());
                }
//                TODO vmikhailov remove commented code
//                Console.WriteLine(pi.ToString());
            }
            return parameters;
        }
    }
}