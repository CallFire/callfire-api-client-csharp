using System;
using CallfireApiClient.Api.Common.Model;


namespace CallfireApiClient
{
    /// <summary>
    /// Callfire API exception is thrown by client in case of 4xx or 5xx HTTP code response
    /// <summary>/
    public class CallfireApiException : Exception
    {
        /// <summary>
        /// Gets or sets detailed error message with HTTP code, help link, etc.
        /// </summary>
        /// <value>The API error message.</value>
        public ErrorMessage ApiErrorMessage { get; set; }

        public CallfireApiException(ErrorMessage apiErrorMessage)
        {
            ApiErrorMessage = apiErrorMessage;
        }

        public override string ToString()
        {
            return string.Format("[CallfireApiException: ApiErrorMessage={0}]", ApiErrorMessage);
        }
    }
}