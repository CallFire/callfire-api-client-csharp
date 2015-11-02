using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case if platform returns HTTP code 401 - Unauthorized, API Key missing or invalid
    /// </summary>
    public class UnauthorizedException : CallfireApiException
    {
        public UnauthorizedException(ErrorMessage errorMessage)
            : base(errorMessage)
        {
        }
    }
}