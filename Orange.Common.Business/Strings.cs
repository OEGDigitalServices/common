namespace Orange.Common.Business
{
    public class Strings
    {
        public struct AppSettings
        {
            public const string ServicesAuthorizationTokenLifeTimeInSeconds = "ServicesAuthorizationTokenLifeTimeInSeconds";
            public const string TokenHashKey = "TokenHashKey";
            public const string IsTokenEnabled = "IsTokenEnabled";
            public const string EricssonEnrichmentHeaderDecryptKey = "EricssonEnrichmentHeaderDecryptKey";
            public const string EnrichmentHeaderDecryptKey = "EnrichmentHeaderDecryptKey";
            public const string IsTestingEnvironment = "IsTestingEnvironment";
            public const string TestingDial = "TestingDial";
            public const string CaptchaUrl = "CaptchaUrl";
            public const string TokenExpiryHours = "TokenExpiryHours";
        }

        public struct Keys
        {
            public const string Null = "null";
            public const string Zero = "0";
            public const string One = "1";
            public const string Two = "2";
            public const string SecureAPIFullURL = "{0}/api/{1}/{2}";
            public const string OrangeTPSecureAPIURL = "OrangeTPSecureAPIURL";
            public const string Controller = "controller";
            public const string Action = "action";
            public const string Dot = ".";
            public const string Colon = ":";
            public const string SheetExtension = ").xlsx";
            public const string FailedTransactions = "FailedTransactions";
            public const string Amount = "Amount";
            public const string UserName = "UserName";
            public const string InvoiceNumber = "InvoiceNumber";
            public const string VPCReceiptNo = "VPCReceiptNo";
            public const string EAIStatus = "EAIStatus";
            public const string EAIErrorCode = "EAIErrorCode";
            public const string CreatedDate = "CreatedDate";
            public const string EndRenewViaCreditCard = "EndRenewViaCreditCard";
            public const string True = "true";
            public const string False = "false";
            public const string Dial = "dial";
            public const string Password = "password";
            public const string IsEasyLogin = "iseasylogin";
            public const string ChannelName = "channelname";
            public const string ChannelPassword = "channelpassword";
            public const string Htv = "_htv";
            public const string Ctv = "_ctv";
            public const string UserId = "userid";
            public const string MSISDN = "MSISDN";
            public const string XMSISDN = "X-MSISDN";
            public const string Pin = "Pin";
            public const string PaymentSerialNumber = "PaymentSerialNumber";
            public const string Token ="Token";
            public const string SecretKey = "SecretKey";
            public const string CaptchaThreshold = "CaptchaThreshold";
            public const string IsCaptchaEnabled = "isCaptchaEnabled";
            public const string CaptchaUrl = "CaptchaUrl";
        }

        public struct OrangeServicesSecuirtyErrorMessages
        {
            public const string Null = "null";
            public const string Zero = "0";
            public const string One = "1";
            public const string Two = "2";
        }

        public struct CustomerTypes
        {
            public const string Corporate = "corporate";
        }
        public struct ErrorDescriptions
        {
            public const string TokenInvalid = "The access token is invalid";
            public const string TokenInvalidErrorLog = "The access token is invalid in {{url}} URL";
            public const string TokenExpired = "The access token is expired";
            public const string TokenExpiredErrorLog = "The access token is expired in {{url}} URL";
            public const string UserIsNotSetErrorLog = "The User is not set in {{url}} URL";
        }
        public struct PropertyNames
        {
            public const string Dial = "Dial";
            public const string Email = "Email";
        }
        public struct APIs
        {
            public const string CommonSecureIntegrationBusURL = "CommonSecureIntegrationBusURL";
            public const string OrangeTPSecureIntegrationBusURL = "OrangeTPSecureIntegrationBusURL";
            public const string OrangeTriplePlayIdentifyUser = "Account/IdentifyUser";
        }
        public struct Verbs
        {
            public const string Post = "POST";

        }
    }
}
