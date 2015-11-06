using System;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// API credentials
    /// </summary>
    public class ApiCredentials : CallfireModel
    {
        public long Id { get; }

        public String Name { get; set; }

        public String Username { get; }

        public String Password { get; }

        public Boolean Enabled { get; set; }

        public override string ToString()
        {
            return string.Format("[ApiCredentials: Id={0}, Name={1}, Username={2}, Password={3}, Enabled={4}]",
                Id, Name, Username, Password, Enabled);
        }
    }
}
