

namespace CallfireApiClient.Api.Common.Model
{
    public class LocalTime : CallfireModel
    {
        public int? Hour { get; set; }

        public int? Minute { get; set; }

        public int? Second { get; set; }

        public override string ToString()
        {
            return string.Format("[LocalTime: Hour={0}, Minute={1}, Second={2}]", Hour, Minute, Second);
        }
    }
}

