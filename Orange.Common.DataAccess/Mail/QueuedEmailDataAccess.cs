using Orange.Common.EntityFramework;
using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orange.Common.DataAccess
{
    public class QueuedEmailDataAccess : IQueuedEmailDataAccess
    {
        private readonly IEntityFramworkUtilties _iEntityFrameworkUtilities;
        public QueuedEmailDataAccess(IEntityFramworkUtilties iEntityFramworkUtilities)
        {
            _iEntityFrameworkUtilities = iEntityFramworkUtilities;
        }
        public bool Add(QueuedEmail email)
        {
            return _iEntityFrameworkUtilities.SaveChanges<NotificationsModel>(dbModel => Add(dbModel, email));
        }
        private void Add(NotificationsModel dbModel, QueuedEmail email)
        {
            email.CreatedDate = DateTime.Now;
            dbModel.QueuedEmails.Add(email);
        }
        public List<QueuedEmail> GetQueuedEmails(int maxRecords)
        {
            return _iEntityFrameworkUtilities.GetEntities<QueuedEmail, NotificationsModel>(dbModel => GetQueuedEmails(dbModel, maxRecords));
        }
        private List<QueuedEmail> GetQueuedEmails(NotificationsModel dbModel, int maxRecords)
        {
            return dbModel.QueuedEmails.Where(qe => !qe.SentDate.HasValue).OrderByDescending(qe => qe.CreatedDate).Take(maxRecords).ToList();
        }
        public bool SetNotificationsAsSent(List<QueuedEmail> emails)
        {
            return _iEntityFrameworkUtilities.SaveChanges<NotificationsModel>(dbModel => SetNotificationsAsSent(dbModel, emails));
        }

        private bool SetNotificationsAsSent(NotificationsModel dbModel, List<QueuedEmail> emails)
        {
            emails.ForEach(mail => dbModel.Entry(mail).State = System.Data.Entity.EntityState.Deleted);
            dbModel.SaveChanges();
            return true;
        }
    }
}
