using CallfireApiClient.Api.Common.Model;

namespace CallfireApiClient.Api.Account.Model
{
    /// <summary>
    /// CallerID number
    /// </summary>
    public class CallerId : CallfireModel
    {
        public string PhoneNumber { get; set; }

        public CallerId()
        {
        }

        public CallerId(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return string.Format("[CallerId: PhoneNumber={0}]", PhoneNumber);
        }
    }
}