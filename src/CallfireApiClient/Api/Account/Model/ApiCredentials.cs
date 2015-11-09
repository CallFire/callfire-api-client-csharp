using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// API credentials
    /// </summary>
    public class ApiCredentials : CallfireModel
    {
        public long? Id { get; private set; }

        public string Name { get; set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public bool? Enabled { get; set; }

        public override string ToString()
        {
            return string.Format("[ApiCredentials: Id={0}, Name={1}, Username={2}, Password={3}, Enabled={4}]",
                Id, Name, Username, Password, Enabled);
        }
    }
}
