using System;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using NUnit.Framework;

namespace CallfireApiClient.IntegrationTests.Api.CallsTexts
{
    [TestFixture, Ignore("temporary disabled")]
    public class CallsApiIntegrationTest : AbstractIntegrationTest
    {

        [Test]
        public void GetCall()
        {
            Call call = Client.CallsApi.Get(1, "id,toNumber,state");
            Console.WriteLine("Call: " + call);

            Assert.AreEqual(1, call.Id);
            Assert.AreEqual("18088395900", call.ToNumber);
            Assert.AreEqual(StateType.FINISHED, call.State); 
        }

        [Test]
        public void FindCalls()
        {
            var request = new FindCallsRequest
			{
				States = { StateType.FINISHED, StateType.READY },
				IntervalBegin = DateTime.UtcNow.AddMonths(-2),
				IntervalEnd = DateTime.UtcNow,
				Limit = 3
			};

            Page<Call> calls = Client.CallsApi.Find(request);
            Console.WriteLine("Calls: " + calls);

            Assert.AreEqual(3, calls.Items.Count);
        }

        [Test]
        public void SendCall()
        {
            var recipient1 = new CallRecipient { ContactId = 463633187003, LiveMessage = "testMessage" };
			var recipient2 = new CallRecipient { ContactId = 463633187003, LiveMessage = "testMessage" };
            var recipients = new List<CallRecipient> { recipient1, recipient2 };
			
            IList<Call> calls = Client.CallsApi.Send(recipients, null, "items(id,fromNumber,state)");
            Console.WriteLine("Calls: " + calls);

            Assert.AreEqual(2, calls.Count);
            Assert.NotNull(calls[0].Id);
            Assert.IsNull(calls[0].CampaignId);
            Assert.AreEqual(StateType.READY, calls[0].State);
        }
    }
}

