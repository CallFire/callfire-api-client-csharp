
namespace CallfireApiClient.Api.Common.Model
{
    public class ErrorMessage : CallfireModel
    {
        public int HttpStatusCode { get; set; }

        public int InternalCode { get; set; }

        public string Message { get; set; }

        public string DeveloperMessage { get; set; }

        public string HelpLink { get; set; }

        public ErrorMessage()
        {
        }

        public ErrorMessage(int httpStatusCode, string message, string helpLink)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
            HelpLink = helpLink;
        }

        public override string ToString()
        {
            return string.Format("[ErrorMessage: HttpStatusCode={0}, InternalCode={1}, Message={2}, DeveloperMessage={3}, HelpLink={4}]",
                HttpStatusCode, InternalCode, Message, DeveloperMessage, HelpLink);
        }
    }
}

