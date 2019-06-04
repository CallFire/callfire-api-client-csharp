using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;

namespace CallfireApiClient.IntegrationTests.Api
{
    public class AbstractIntegrationTest
    {
        protected CallfireClient Client;

        private string apiUserName;

        private string apiUserPassword;

        public AbstractIntegrationTest()
        {
            ReadAllSettings();
            Client = new CallfireClient(apiUserName, apiUserPassword);
        }

        private void ReadAllSettings()
        {
            try
            {   
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    apiUserName = appSettings["testLogin"];
                    apiUserPassword = appSettings["testPassword"];
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public static string GetFullPath(string path)
        {
            return Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName + path;
        }
    }
}

