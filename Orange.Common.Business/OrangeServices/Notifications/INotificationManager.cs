using Orange.Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface INotificationManager
    {
        bool AddNotification(string body, string subject, string from, string to, string ccMail, Stream attachement, string attachementName, CultureInfo cultureInfo);
    }
}
