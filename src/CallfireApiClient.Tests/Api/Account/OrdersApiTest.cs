using System;
using NUnit.Framework;
using CallfireApiClient.Api.Account.Model;

namespace CallfireApiClient.Tests.Api.Account
{
    public class OrdersApiTest : AbstractApiTest
    {
        [Test()]
        public void GetPayload()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getAccount.json");
            Console.WriteLine("=" + expectedJson);
            //  MockRestResponse(expectedJson);

            //  UserAccount account = Client.MeApi.GetAccount();
            //  Assert.That(Serializer.Serialize(account), Is.StringMatching(expectedJson));
        }

    }
}

