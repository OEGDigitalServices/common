using System.Globalization;
using System.IO;

namespace Orange.Common.Business
{
    public abstract class NotificationManager : INotificationManager
    {
        public abstract bool AddNotification(string body, string subject, string from, string to, string ccMail, Stream attachement, string attachementName, CultureInfo cultureInfo, string resourceKey);
    }
}
