using CallfireApiClient.Api.Common.Model;
using System;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case error has occurred in client.
    /// </summary>
    public class CallfireClientException : Exception
    {
        public CallfireClientException(string message)
            : base(message)
        {
        }

        public CallfireClientException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}