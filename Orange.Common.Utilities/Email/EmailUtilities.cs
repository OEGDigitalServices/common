using System;
using System.Net.Mail;

namespace Orange.Common.Utilities
{
    public class EmailUtilities : IEmailUtilities
    {
        private readonly EmailConfigurations _emailConfigurations;
        private readonly ILogger _logger;

        public EmailUtilities(EmailConfigurations emailConfigurations, ILogger logger)
        {
            _emailConfigurations = emailConfigurations;
            _logger = logger;
        }
        public bool SendEmail(string emailToAddress, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_emailConfigurations.EmailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(_emailConfigurations.SMTPAddress, _emailConfigurations.Port))
                    {
                        //smtp.Credentials = new NetworkCredential(_emailConfigurations.EmailFromAddress, _emailConfigurations.EmailFromPassword);
                        //smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace, e, false);
                return false;
            }
        }
    }
}
