namespace CallfireApiClient.Api.Common.Model
{
    public class Page<T> : ListHolder<T>
    {
        public long Limit { get; set; }

        public long Offset { get; set; }

        public long TotalCount { get; set; }

        public override string ToString()
        {
            return string.Format("[Page: Limit={0}, Offset={1}, TotalCount={2}]", Limit, Offset, TotalCount);
        }
    }
}
