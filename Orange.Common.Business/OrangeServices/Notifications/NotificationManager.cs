using System.Globalization;
using System.IO;
using System.Net.Mail;

namespace Orange.Common.Business
{
    public abstract class NotificationManager : INotificationManager
    {
        public abstract bool AddNotification(string body, string subject, string from, string to, string ccMail, Stream attachement, string attachementName, CultureInfo cultureInfo);
    }
}
