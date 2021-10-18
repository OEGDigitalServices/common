using Orange.Common.EntityFramework;
using System.Collections.Generic;

namespace Orange.Common.DataAccess
{
    public interface IQueuedEmailDataAccess
    {
        bool Add(QueuedEmail email);
        List<QueuedEmail> GetQueuedEmails(int maxRecords);
        bool SetNotificationsAsSent(List<QueuedEmail> mails);
    }
}
