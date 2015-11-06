using System;
using System.Runtime.Serialization;
using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model.Request
{
    /// <summary>
    /// Request object for POST /admin/callerids/{callerid}/verification-code
    /// <summary>
    public class CallerIdVerificationRequest : CallfireModel
    {
        public string VerificationCode { get; set; }

        [IgnoreDataMemberAttribute]
        public string CallerId { get; set; }

        public override string ToString()
        {
            return string.Format("[CallerIdVerificationRequest: VerificationCode={0}, CallerId={1}]", VerificationCode, CallerId);
        }
    }
}