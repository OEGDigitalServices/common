using System.Collections.Generic;

namespace Orange.Common.Entities.OrangeTriplePlay
{
    public class IdentifyUserResponse
    {
        public enum ErrorCodes
        {
            Success = 0,
            NullResponse = 1,
            UnspecifiedError = 2,
            ServiceDown = 3,
            TechnicalError = 4,
        }
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string UCID { get; set; }
        public List<string> UserNames { get; set; }
    }
}
