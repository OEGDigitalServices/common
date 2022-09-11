namespace Orange.Common.Profile.Membership
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailConfirmed { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string LegacyUserName { get; set; }

        public string LegacyPasswordHash { get; set; }

        [Required]
        [StringLength(256)]
        public string LoweredUserName { get; set; }

        public bool IsMobinil { get; set; }

        public bool IsCPUser { get; set; }

        public DateTime LastActivityDate { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string LoweredEmail { get; set; }

        [StringLength(256)]
        public string CPEmail { get; set; }

        [StringLength(256)]
        public string LoweredCPEmail { get; set; }

        [StringLength(256)]
        public string PasswordQuestion { get; set; }

        [StringLength(128)]
        public string PasswordAnswer { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public DateTime LastUserNameChangedDate { get; set; }

        public DateTime LastLockoutDate { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime FailedPasswordAttemptWindowStart { get; set; }

        public int FailedPasswordAnswerAttemptCount { get; set; }

        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Profession { get; set; }

        [StringLength(50)]
        public string Education { get; set; }

        [StringLength(50)]
        public string PreferredAddress { get; set; }

        [StringLength(50)]
        public string FacebookName { get; set; }

        [StringLength(50)]
        public string RatePlanID { get; set; }

        [StringLength(20)]
        public string PreferredLanguage { get; set; }

        public int? IDType { get; set; }

        [StringLength(50)]
        public string IDNumber { get; set; }

        public int? City { get; set; }

        [StringLength(50)]
        public string StreetName { get; set; }

        [StringLength(50)]
        public string BuildingNumber { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        public bool? IsPasswordExpired { get; set; }

        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        public bool? IsGuest { get; set; }
    }
}
