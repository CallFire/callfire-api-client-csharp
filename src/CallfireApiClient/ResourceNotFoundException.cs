
namespace CallfireApiClient
{
    /// <summary>
    /// Exception thrown in case if platform returns HTTP code 404 - NOT FOUND, the resource requested does not exist
    /// </summary>
    public class ResourceNotFoundException : CallfireApiException
    {
        public ResourceNotFoundException(ErrorMessage errorMessage)
            : base(errorMessage)
        {
        }
    }
}