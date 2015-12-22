using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient
{
    /// <summary>
    /// Exception is used by Callfire model validation methods
    /// </summary>
    public class ModelValidationException : CallfireApiException
    {
        public ModelValidationException(ErrorMessage errorMessage) : base(errorMessage)
        {
        }
    }
}