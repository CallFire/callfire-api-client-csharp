using System;
using System.Collections.Generic;
using CallfireApiClient.Api.CallsTexts.Model;

namespace CallfireApiClient.Api.Campaigns.Model
{
    public class TextBroadcast : Broadcast
    {
        public string Message { get; set; }

        public BigMessageStrategy? BigMessageStrategy { get; set; }

        public IList<TextRecipient> Recipients  { get; set; }

        public IList<Media> Media { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} TextBroadcast: Message={1}, BigMessageStrategy={2}, Recipients={3}, Media={4}]", 
                base.ToString(), Message, BigMessageStrategy, Recipients, Media);
        }
    }
}

