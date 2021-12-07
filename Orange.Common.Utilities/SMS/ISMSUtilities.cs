namespace Orange.Common.Utilities
{
    public interface ISMSUtilities
    {
        bool SendSMSMessage(string MobileNumber, string msgBody, string language, string alias);
    }
}
