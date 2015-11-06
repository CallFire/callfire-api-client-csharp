using System;
using CallfireApiClient.Api.Common.Model;
using System.Collections.Generic;
using System.Linq;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// User's account representation
    /// <summary>/
    public class UserAccount : CallfireModel
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<UserPermission> Permissions { get; set; }

        public enum UserPermission
        {
            API,
            ACCOUNT_HOLDER,
            AGENT,
        }

        public override string ToString()
        {
            return string.Format("[Account: Id={0}, Email={1}, Name={2}, FirstName={3}, LastName={4}, Permissions={5}]",
                Id, Email, Name, FirstName, LastName, String.Join(",", Permissions ?? Enumerable.Empty<UserPermission>()));
        }
    }
}
