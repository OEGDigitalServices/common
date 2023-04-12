namespace Orange.Common.Entities
{
    public class WalletBalanceInquiryOutput
    {
        public enum ErrorCodes
        {
            Success = 0,
            ChannelIsNull = 1,
            DBExceptionWhileCheckingChannelInfo = 2,
            InvalidChannelInfo = 3,
            NoPrivilegeForChannel = 4,
            DialIsNull = 5,
            PinIsNull = 6,
            PasswordIsNull = 7,
            InvalidUserNameOrPassword = 8,
            XMLMessageElementNotSuccess = 9,
            NullResponse = 10,
            XMLMessageElementNotExist = 11,
            ServiceException = 12,
            XMLTXNStatusElementNotExist = 13,
            XMLTXNstatusElementNotSuccess = 14,
            LockedAccount = 15,
            MaximumAllowedCallsExceeded = 16,
            InvalidPin = 17,
            WrongPin = 18,
            ResetPin = 19,
            PINExpired = 20,
            Suspended = 21
        }
        public ErrorCodes ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
