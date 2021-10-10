namespace Orange.Common.Utilities
{
    public interface IEmailUtilities
    {
        bool SendEmail(string emailToAddress, string subject, string body);
    }
}
