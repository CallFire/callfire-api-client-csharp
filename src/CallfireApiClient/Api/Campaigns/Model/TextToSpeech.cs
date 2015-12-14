using System;
using CallfireApiClient.Api.Common.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class TextToSpeech : CallfireModel
    {
        public Voice Voice { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("[TextToSpeech: Voice={0}, Message={1}]",
                Voice, Message);
        }
    }
}

