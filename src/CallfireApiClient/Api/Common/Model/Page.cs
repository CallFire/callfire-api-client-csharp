namespace CallfireApiClient.Api.Common.Model
{
    public class Page<T> : ListHolder<T>
    {
        public long? Limit { get; set; }

        public long? Offset { get; set; }

        public long? TotalCount { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} [Page: Limit={1}, Offset={2}, TotalCount={3}] ]", base.ToString(),
                Limit, Offset, TotalCount);
        }
    }
}
