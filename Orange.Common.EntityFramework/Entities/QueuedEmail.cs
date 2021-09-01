using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework
{
    public class QueuedEmail
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] AttachmentFile { get; set; }
        public string AttachmentFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SentTries { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
