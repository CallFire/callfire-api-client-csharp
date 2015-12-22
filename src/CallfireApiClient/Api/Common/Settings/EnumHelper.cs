using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace CallfireApiClient.Api.Common.Settings
{
    public static class EnumHelper
    {
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
    }
}
