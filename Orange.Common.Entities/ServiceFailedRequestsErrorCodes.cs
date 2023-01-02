namespace Orange.Common.Entities
{
    public enum ServiceFailedRequestsErrorCodes
    {
        Success = 0,
        DialIsNull = 1,
        DialIsInvalid = 2,
        PasswordIsNull = 3,
        ChannekInfoIsInvalid = 4,
        ChannelNameIsNull = 5,
        ChannelPasswordIsNull = 6,
        AuthenticationFailed = 7,
        MethodHasInsufficientPrivilege = 8,
        TokenIsInvalid = 9,
        InvalidCtvValue = 10,
        UserIdIsNull = 11,
        DialIsNotBelongToAccount = 12,
        DialAndPasswordAreNull = 13,
        ChannelNameAndPasswordAreNull = 14,
        LanguageIsEmpty = 15,
        LanguageIsInValid = 16,
        PinIsNullOrEmpty = 17,
        PinNotValid = 18,
        AccountIsLocked = 19,
        WalletBalanceUnspecifiedError = 20,
        AccountNumberAndPasswordAreNulls = 21,
        AccountNumberIsNullOrEmpty = 22,
        DSLTokenIsInvalid = 23,
        InputIsNull = 24
    }
}
