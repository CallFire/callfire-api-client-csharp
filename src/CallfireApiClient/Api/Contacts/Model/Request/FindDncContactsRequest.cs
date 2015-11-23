using System;
using CallfireApiClient.Api.Common.Model.Request;

namespace CallfireApiClient.Api.Contacts.Model.Request
{
    public class FindDncContactsRequest : FindRequest
    {
        public string prefix { get; set; }
        public long? dncListId { get; set; }
        public string dncListName { get; set; }
        public bool? callDnc { get; set; }
        public bool? textDnc { get; set; }
    }
}

