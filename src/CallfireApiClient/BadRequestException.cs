using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case if platform returns HTTP code 400 - Bad request, the request was formatted improperly.
    /// </summary>
    public class BadRequestException : CallfireApiException
    {
        public BadRequestException(ErrorMessage errorMessage)
            : base(errorMessage)
        {
        }
    }
}