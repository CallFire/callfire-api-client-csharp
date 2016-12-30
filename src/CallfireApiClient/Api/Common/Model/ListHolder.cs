using System.Collections.Generic;

namespace CallfireApiClient.Api.Common.Model
{
    public class ListHolder<T> : CallfireModel
    {
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
            return string.Format("[ListHolder: Items={0}]", Items?.ToPrettyString());
        }
    }
}