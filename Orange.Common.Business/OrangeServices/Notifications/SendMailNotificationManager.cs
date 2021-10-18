using Orange.Common.DataAccess;
using Orange.Common.EntityFramework;
using Orange.Common.Utilities;
using System;
using System.Globalization;
using System.IO;
using System.Web;

namespace Orange.Common.Business
{
    public class SendMailNotificationManager : NotificationManager
    {
        #region Probs
        private readonly IQueuedEmailDataAccess _queuedEmailDataAccess;
        private readonly ILogger _logger;
        private readonly IUtilities _utilities;
        #endregion
        #region Ctor
        public SendMailNotificationManager(IQueuedEmailDataAccess queuedEmailDataAccess, ILogger logger, IUtilities utilities)
        {
            _queuedEmailDataAccess = queuedEmailDataAccess;
            _logger = logger;
            _utilities = utilities;
        }
        #endregion

        public override bool AddNotification(string body, string subject, string from, string to, string ccMail, Stream attachement, string attachementName, CultureInfo cultureInfo)
        {
            try
            {
                QueuedEmail email = new QueuedEmail();
                email.Subject = subject;
                email.Body = PrepareMessageBody(body, cultureInfo);
                email.From = from;
                email.To = to;
                email.CC = ccMail;
                if (attachement != null)
                {
                    var fileBinary = new byte[attachement.Length];
                    email.AttachmentFile = fileBinary;
                    email.AttachmentFileName = attachementName;
                }
                return _queuedEmailDataAccess.Add(email);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp);
                return false;
            }
           
        }
        private string PrepareMessageBody(string body, CultureInfo cultureInfo)
        {
            string str = HttpContext.GetGlobalResourceObject(Utilities.Strings.Mails.CommonGlobalResource, Utilities.Strings.Mails.MainMailContainer, cultureInfo) as string;
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            str = str.Replace(Utilities.Strings.Mails.BodyMailReplace, body);
            str = str.Replace(Utilities.Strings.Mails.SiteUrl, _utilities.PublicSiteURL);
            return str;
        }
    }
}
