using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Orange.Common.EntityFramework
{
    public partial class OrangeDSLContext : DbContext
    {
        public OrangeDSLContext()
            : base("name=OrangeDSLActionLogsContext")
        {
        }

        public virtual DbSet<DSLToken> DSLTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
