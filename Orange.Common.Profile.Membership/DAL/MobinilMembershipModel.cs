using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Orange.Common.Profile.Membership
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    internal partial class MobinilMembershipModel : DbContext
    {
        public MobinilMembershipModel()
            : base("name=MobinilProfileConnection")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public virtual ObjectResult<int?> CreateUserWithSubDial(User user, string serverIP, string userAgent, string channel, string userIP)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>
            {
                new SqlParameter("@Id",user.Id),
                new SqlParameter("@UserName",user.UserName),
                new SqlParameter("@PasswordHash",!string.IsNullOrEmpty(user.PasswordHash) ? user.PasswordHash: String.Empty),
                new SqlParameter("@LoweredUserName",user.LoweredUserName),
                new SqlParameter("@IsMobinil",user.IsMobinil),
                new SqlParameter("@IsCPUser",user.IsCPUser),
                new SqlParameter("@RatePlanID",!string.IsNullOrEmpty(user.RatePlanID) ? user.RatePlanID: String.Empty),
                new SqlParameter("@Email",!string.IsNullOrEmpty(user.Email) ? user.Email: String.Empty),
                new SqlParameter("@LoweredEmail",!string.IsNullOrEmpty(user.LoweredEmail) ? user.LoweredEmail: String.Empty),
                new SqlParameter("@CPEmail",!string.IsNullOrEmpty(user.CPEmail) ? user.CPEmail: String.Empty),
                new SqlParameter("@LoweredCPEmail",!string.IsNullOrEmpty(user.LoweredCPEmail) ? user.LoweredCPEmail: String.Empty),
                new SqlParameter("@PasswordQuestion",!string.IsNullOrEmpty(user.PasswordQuestion) ? user.PasswordQuestion: String.Empty),
                new SqlParameter("@PasswordAnswer",!string.IsNullOrEmpty(user.PasswordAnswer) ? user.PasswordAnswer: String.Empty),
                new SqlParameter("@IsApproved",user.IsApproved),
                new SqlParameter("@CreateDate",user.CreateDate),
                new SqlParameter("@FirstName",!string.IsNullOrEmpty(user.FirstName) ? user.FirstName: String.Empty),
                new SqlParameter("@LastName",!string.IsNullOrEmpty(user.LastName) ? user.LastName: String.Empty),
                new SqlParameter("@BirthDate", user.BirthDate.HasValue ? user.BirthDate.Value  : (object)DBNull.Value ),
                new SqlParameter("@ServerIP",!string.IsNullOrEmpty(serverIP) ? serverIP: String.Empty),
                new SqlParameter("@Channel",!string.IsNullOrEmpty(channel) ? channel: String.Empty),
                new SqlParameter("@UserAgent",!string.IsNullOrEmpty(userAgent) ? userAgent: String.Empty),
                new SqlParameter("@UserIP",!string.IsNullOrEmpty(userIP) ? userIP: String.Empty),
                new SqlParameter("@IsGuest",user.IsGuest)

            };

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<int?>("exec CreateUserWithSubDial @Id,@UserName,@PasswordHash ,@LoweredUserName,@IsMobinil,@IsCPUser,@RatePlanID,@Email,@LoweredEmail,@CPEmail,@LoweredCPEmail ,@PasswordQuestion,@PasswordAnswer,@IsApproved ,@CreateDate ,@FirstName ,@LastName ,@BirthDate ,@ServerIP ,@Channel ,@UserAgent ,@UserIP,@IsGuest ", sqlParams.ToArray());
        }

        public virtual ObjectResult<int?> MigrateProfile(Guid userID, string newUserName, string oldUserName, DateTime createDate)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>
            {
                new SqlParameter("@Id",userID),
                new SqlParameter("@NewUserName",newUserName),
                new SqlParameter("@OldUserName",oldUserName),
                new SqlParameter("@CreateDate",createDate)
            };

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<int?>("exec MigrateProfile @Id,@NewUserName,@OldUserName ,@CreateDate", sqlParams.ToArray());
        }

        public virtual ObjectResult<int?> DeleteUserWithSubDials(string userName)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>
            {
                new SqlParameter("@UserName",userName)
            };

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<int?>("exec DeleteUserWithSubDials @UserName", sqlParams.ToArray());
        }
    }
}
