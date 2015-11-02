using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case if platform returns HTTP code 403 - Forbidden, insufficient permissions
    /// </summary>
    public class AccessForbiddenException : CallfireApiException
    {
        public AccessForbiddenException(ErrorMessage errorMessage)
            : base(errorMessage)
        {
        }
    }
}