using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CallfireApiClient.Api.Common.Model
{
    public class ListHolder<T> : CallfireModel
    {
        [JsonProperty(Order = -2)]
        public IList<T> Items { get; set; }

        public ListHolder()
        {
        }

        public ListHolder(IList<T> items)
        {
            Items = items;
        }

        public override string ToString()
        {
            return string.Format("[ListHolder: Items={0}]", Items?.ToPrettyString());
        }
    }
}