using CallfireApiClient.Api.Common.Model;

namespace  CallfireApiClient.Api.Contacts.Model.Request
{
    /// <summary>
    /// Abstract request object for controlling calls/texts flags availability
    /// </summary>
    public abstract class CallsTextsRequest : CallfireModel
    {
        /// <summary>
        /// Set Call flag
        /// </summary
        public bool? Call { get; set; }

        /// <summary>
        /// Set Text flag
        /// </summary>
        public bool? Text { get; set; }

        public override string ToString()
        {
            return string.Format("[FindRequest: Call={0}, Text={1}]", Call, Text);
        }
    }
}