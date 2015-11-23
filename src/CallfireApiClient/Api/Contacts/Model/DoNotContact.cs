using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class DoNotContact : CallfireModel
    {
        public string number { get; set; }
        public bool? call { get; set; }
        public bool? text { get; set; }
        public long? listId { get; set; }

        public override string ToString()
        {
            return string.Format("[DoNotContact: number={0}, call={1}, text={2}, listId={3}",
                number, call, text, listId);
        }
    }
}