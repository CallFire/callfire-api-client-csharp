namespace CallfireApiClient.Api.Common.Model
{
    public class ResourceId : CallfireModel
    {
        public long? Id { get; set; }

        public override string ToString()
        {
            return string.Format("[ResourceId: Id={0}]", Id);
        }
    }
}