using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.CallsTexts.Model
{
    public class Media : CallfireModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public long? Created { get; private set; }
        public long? LengthInBytes  { get; private set; }
        public MediaType? MediaType { get; set; }
        public string PublicUrl { get; private set; }

        public override string ToString()
        {
            return string.Format("[Media: Id={0}, Name={1}, Created={2}, LengthInBytes={3}, MediaType={4}, PublicUrl={5}]",
                Id, Name, Created, LengthInBytes, MediaType, PublicUrl);
        }
    }
}

