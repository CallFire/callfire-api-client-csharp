using System;

namespace CallfireApiClient
{
    /// <summary>
    /// Class contains validation static methods
    /// </summary>
    internal static class Validate
    {
        public static void NotBlank(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotNull(object value, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}

