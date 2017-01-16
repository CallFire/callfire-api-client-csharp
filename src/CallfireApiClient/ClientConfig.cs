
namespace CallfireApiClient
{
    /// <summary>
    /// object to include client configuration parameters
    /// <summary>
    public class ClientConfig
    {
        /// <summary>
        /// Returns base URL path for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of base URL path</returns>
        public string ApiBasePath { get; set; }

        /// <summary>
        /// Returns proxy adress for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of proxy adress</returns>
        public string ProxyAddress { get; set; }

        /// <summary>
        /// Returns proxy port for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of proxy port</returns>
        public int ProxyPort { get; set; }

        /// <summary>
        /// Returns proxy login for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of proxy login</returns>
        public string ProxyLogin { get; set; }

        /// <summary>
        /// Returns proxy password for all Callfire's API 2.0 endpoints
        /// <summary>/
        /// <returns>string representation of proxy login</returns>
        public string ProxyPassword { get; set; }
    }
}