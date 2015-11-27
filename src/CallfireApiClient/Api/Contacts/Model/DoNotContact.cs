using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class DoNotContact : CallfireModel
    {
        public string Number { get; set; }
        public bool? Call { get; set; }
        public bool? Text { get; set; }
        public long? ListId { get; set; }

        public override string ToString()
        {
            return string.Format("[DoNotContact: number={0}, call={1}, text={2}, listId={3}",
                Number, Call, Text, ListId);
        }
    }
}