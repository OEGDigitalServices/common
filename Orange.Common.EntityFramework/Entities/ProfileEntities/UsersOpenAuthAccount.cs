namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("UsersOpenAuthAccounts")]
    public partial class UsersOpenAuthAccount
    {
        [Key]
        public int ID { get; set; }

        [StringLength(128)]
        public string ProviderName { get; set; }

        [StringLength(128)]
        public string ProviderUserId { get; set; }

        [StringLength(128)]
        public string ProviderUserName { get; set; }

        [Required]
        public Guid MembershipUserID { get; set; }

        [StringLength(50)]
        public string CreatedAt { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? LastUsedDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletionDate { get; set; }

        public virtual UsersOpenAuthAccountData UsersOpenAuthAccountData { get; set; }
    }
}
