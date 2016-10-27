using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Numbers.Model
{
    public class GoogleAnalytics : CallfireModel
    {
        public string Domain { get; set; }

        public string GoogleAccountId { get; set; }

        public string Category { get; set; }

        public override string ToString()
        {
            return string.Format("[GoogleAnalytics: Domain={0}, GoogleAccountId={1}, Category={2}]", Domain, GoogleAccountId, Category);
        }
    }
}