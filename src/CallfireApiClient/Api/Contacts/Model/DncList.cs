using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Contacts.Model
{
    public class DncList : CallfireModel
    {
        public long? id { get; set; }
        public int? size { get; set; }
        public long? campaignId { get; set; }
        public string name { get; set; }
        public DateTime? created { get; set; }

        public override string ToString()
        {
            return string.Format("[DncList: id={0}, size={1}, campaignId={2}, name={3}, created={4}",
                id, size, campaignId, name, created);
        }
    }
}