using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Orange.Common.EntityFramework.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OrangeServicesModel : DbContext
    {
        public OrangeServicesModel()
            : base("name=OrangeServicesModel")
        {
        }

        public virtual DbSet<ChannelsData> ChannelsDatas { get; set; }
        public virtual DbSet<ChannelsPrivilege> ChannelsPrivileges { get; set; }
        public virtual DbSet<ChannelToken> ChannelTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public virtual ObjectResult<GetTokenByIDResult> GetTokenByID(Nullable<System.Guid> tokenValue)
        {
            var tokenValueParameter = tokenValue.HasValue ?
                new ObjectParameter("TokenValue", tokenValue) :
                new ObjectParameter("TokenValue", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTokenByIDResult>("GetTokenByID", tokenValueParameter);
        }
    }
}
