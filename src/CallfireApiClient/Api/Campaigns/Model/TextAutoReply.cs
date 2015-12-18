using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    /// <summary>
    /// Text auto reply.
    /// </summary>
    public class TextAutoReply : CallfireModel
    {
        /// <summary>
        /// Gets or sets the unique ID of text auto reply.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the phone number to configure an auto reply message.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the keyword associated with account.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the matching text is either null or empty which represents all matches.
        /// All other text, for example 'rocks', will be matched as case insensitive whole words..
        /// </summary>
        public string Match { get; set; }

        /// <summary>
        /// Gets or sets the templated message to return as auto reply (ex: 'Here is an echo - ${text.message}').
        /// </summary>
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("[TextAutoReply: Id={0}, Number={1}, Keyword={2}, Match={3}, Message={4}]", 
                Id, Number, Keyword, Match, Message);
        }
    }
}

