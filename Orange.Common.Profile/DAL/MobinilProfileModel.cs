using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Orange.Common.Profile
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using Orange.Common.EntityFramework.Entities;

    public partial class MobinilProfileModel : DbContext
    {
        public MobinilProfileModel()
            : base("name=MobinilProfileConnection")
        {
        }

        public virtual DbSet<UserDial> UserDials { get; set; }
    }
}