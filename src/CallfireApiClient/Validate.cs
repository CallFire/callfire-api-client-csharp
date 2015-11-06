using System;

namespace CallfireApiClient
{
    /// <summary>
    /// Class contains validation static methods
    /// </summary>
    internal static class Validate
    {
        public static void notBlank(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message);
            }
        }

        public static void notNull(object value, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}

