using System;
using NUnit.Framework;

namespace CallfireApiClient.Tests
{
    public class ClientUtilsTest
    {
        [Test()]
        public void ReplaceFirst()
        {
            const string textToReplace = "/me/callerids/{}/test/{}";

            string firstReplace = textToReplace.ReplaceFirst(ClientConstants.PLACEHOLDER, "1");
            Assert.That(firstReplace, Is.EqualTo("/me/callerids/1/test/{}"));

            string secondReplace = firstReplace.ReplaceFirst(ClientConstants.PLACEHOLDER, "2");
            Assert.That(secondReplace, Is.EqualTo("/me/callerids/1/test/2"));
        }
    }
}

