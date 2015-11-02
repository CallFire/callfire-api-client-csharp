using System;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// User's account representation
    /// <summary>/
    public class Account : CallfireModel
    {
        public Long id { get; set; }

        public String email { get; set; }

        public String name { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public List<UserPermission> permissions { get; set; }

        public enum UserPermission
        {
            API,
            ACCOUNT_HOLDER,
            AGENT,
        }
    }
}
