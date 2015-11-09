using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace CallfireApiClient.Api.Common.Model
{
    public class ListHolder<T> : CallfireModel
    {
        [JsonProperty(Order = -2)]
        public IList<T> Items { get; set; }

        public ListHolder()
        {
            Items = new List<T>();
        }

        public ListHolder(IList<T> items)
        {
            Items = items;
        }

        public override string ToString()
        {
            return string.Format("[ListHolder: Items={0}]", String.Join(",", Items));
        }
    }
}