using CallfireApiClient;
using CallfireApiClient.Api.Common.Model;
using CallfireApiClient.Api.Webhooks.Model;
using CallfireApiClient.Api.Webhooks.Model.Request;

public class ApiClientSample
{
    public static void Main(string[] args)
    {
        var client = new CallfireClient("api_login", "api_password");
        var request = new FindWebhooksRequest
        {
            Name = "my webhook",
            Resource = ResourceType.TEXT_BROADCAST,
            Event = ResourceEvent.STARTED,
            Callback = "https://myservice/callback",
            Enabled = true,
            Offset = 0,
            Limit = 10,
            Fields = "items(id,name,callback)"
        };
        Page<Webhook> webhooks = client.WebhooksApi.Find(request);
    }
}
