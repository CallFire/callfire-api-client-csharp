using System;


namespace CallfireApiClient
{
    /// <summary>
    /// Client constants
    ///</summary>
    public static class ClientConstants
    {
        public const string LOG_DATETIME_PATTERN = "yyyy-MM-dd HH:mm:ss.fff";
        public const string LOG_TRACE_SOURCE_NAME = "CallfireApiClient";
        public const string LOG_FILE_LISTENER_NAME = "CallfireLogFile";

        public const string CONFIG_API_BASE_PATH = "CallFireBasePath";
        public const string CONFIG_CLIENT_NAME = "CallFireClientVersion";

        public const string PLACEHOLDER = "{}";
        public const string GENERIC_HELP_LINK = "https://answers.callfire.com/hc/en-us";

        public static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}