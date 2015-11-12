using System;
using System.Collections.Generic;
using System.Linq;
using CallfireApiClient.Api.CallsTexts.Model;
using Newtonsoft.Json;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class ContactHistory
    {
        public long Id { get; private set; }

        public IList<Call> Calls { get; private set; }

        public IList<Text> Texts { get; private set; }

        public override string ToString()
        {
            return string.Format("[ContactHistory: Id={0}, Calls={1}, Texts={2}]", Id, Calls?.ToPrettyString(),
                Texts?.ToPrettyString());
        }
    }
}

