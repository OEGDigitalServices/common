namespace Orange.Common.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Orange.Common.EntityFramework;

    public partial class CommonModel : DbContext
    {
        public CommonModel()
            : base("name=CommonModel")
        {
        }

        public virtual DbSet<ServicesFailedRequest> ServicesFailedRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
