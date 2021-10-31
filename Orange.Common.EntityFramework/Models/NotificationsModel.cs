using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework
{
    public class NotificationsModel : DbContext
    {
        public NotificationsModel()
            : base("name=NotificationsModel")
        {
        }
        public virtual DbSet<QueuedEmail> QueuedEmails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}
