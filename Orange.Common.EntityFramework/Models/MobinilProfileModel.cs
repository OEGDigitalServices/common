using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Orange.Common.EntityFramework
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
        public virtual DbSet<UsersLanguagePrefrence> UsersLanguagePrefrences { get; set; }
        public virtual DbSet<UsersOpenAuthAccount> UsersOpenAuthAccounts { get; set; }
        public virtual DbSet<UsersOpenAuthAccountData> UsersOpenAuthAccountData { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressAttribute> AddressAttributes { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<ChannelData> ChannelsInfo { get; set; }
        public virtual DbSet<ChannelsPrivilege> ChannelsPrivileges { get; set; }
        public virtual DbSet<MobileDevicesToken> MobileDevicesTokens { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<SSOConsumer> SSOConsumers { get; set; }
        public virtual DbSet<CallBlockerContact> CallBlockerContacts { get; set; }
        public virtual DbSet<ChannelToken> ChannelTokens { get; set; }
        public virtual DbSet<EasyLoginRequest> EasyLoginRequests { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersOpenAuthAccount>().HasKey(u => u.ID);
            modelBuilder.Entity<UsersOpenAuthAccount>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UsersOpenAuthAccount>()
          .HasOptional(e => e.UsersOpenAuthAccountData)
          .WithRequired(e => e.UsersOpenAuthAccount)
          .WillCascadeOnDelete();

            modelBuilder.Entity<UserDial>().HasKey(u => u.ID);
            modelBuilder.Entity<UserDial>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Address>().HasKey(u => u.Id);
            modelBuilder.Entity<Address>().Property(u => u.Id)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Country>().HasKey(u => u.Id);
            modelBuilder.Entity<Country>().Property(u => u.Id)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<StateProvince>().HasKey(u => u.Id);
            modelBuilder.Entity<StateProvince>().Property(u => u.Id)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ChannelData>().HasKey(u => u.ID);
            modelBuilder.Entity<ChannelData>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ChannelsPrivilege>().HasKey(u => u.ID);
            modelBuilder.Entity<ChannelsPrivilege>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MobileDevicesToken>().HasKey(u => u.Id);
            modelBuilder.Entity<MobileDevicesToken>().Property(u => u.Id)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Token>().HasKey(u => u.ID);
            modelBuilder.Entity<Token>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SSOConsumer>().HasKey(u => u.ID);
            modelBuilder.Entity<SSOConsumer>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CallBlockerContact>().HasKey(u => u.ID);
            modelBuilder.Entity<CallBlockerContact>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<ChannelToken>().HasKey(u => u.ID);
            modelBuilder.Entity<ChannelToken>().Property(u => u.ID)
              .HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);
        }
    }
}