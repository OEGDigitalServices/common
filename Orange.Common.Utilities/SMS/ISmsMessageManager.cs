using Orange.Common.Entities;

namespace Orange.Common.Utilities
{
    public interface ISmsMessageManager
    {
        int Initialize(string Username, string Password);
        int SendMessage(MessageDetails Msg);
    }
}
