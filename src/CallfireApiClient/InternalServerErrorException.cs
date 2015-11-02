using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case if platform returns HTTP code 500 - Internal Server Error
    /// </summary>
    public class InternalServerErrorException : CallfireApiException
    {
        public InternalServerErrorException(ErrorMessage errorMessage)
            : base(errorMessage)
        {
        }
    }
}