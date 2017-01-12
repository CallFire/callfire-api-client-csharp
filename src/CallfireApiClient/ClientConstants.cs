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
        public const string API_BASE_PATH_DEFAULT_VALUE = "https://api.callfire.com/v2";

        public const string CONFIG_CLIENT_NAME = "CallFireClientVersion";

        public const string PROXY_ADDRESS_PROPERTY = "com.callfire.api.client.proxy.address";
        public const string PROXY_CREDENTIALS_PROPERTY = "com.callfire.api.client.proxy.credentials";

        public const int DEFAULT_PROXY_PORT = 8080;

        public const string PLACEHOLDER = "{}";
        public const string GENERIC_HELP_LINK = "https://answers.callfire.com/hc/en-us";

        public static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string DEFAULT_FILE_CONTENT_TYPE = "application/octet-stream";
    }
}