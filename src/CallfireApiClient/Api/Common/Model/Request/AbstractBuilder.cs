using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Common.Model.Request
{
    /// <summary>
    /// Common builder for request objects
    /// <summary>/
    /// <typeparam name="R">Request type</typeparam>
    public class AbstractBuilder<R> where  R: CallfireModel
    {
        protected R Request;

        protected AbstractBuilder(R request)
        {
            Request = request;
        }

        /// <summary>
        /// Build request
        /// </summary>
        /// <returns>find request pojo</returns>
        public R build()
        {
            return Request;
        }
    }
}