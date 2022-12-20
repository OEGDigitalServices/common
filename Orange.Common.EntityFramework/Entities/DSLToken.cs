using System;
using System.ComponentModel.DataAnnotations;

namespace Orange.Common.EntityFramework
{
    public partial class DSLToken
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Dial { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string LandLineNumber { get; set; }

        [StringLength(100)]
        public string UCID { get; set; }

        [StringLength(100)]
        public string CustomerId { get; set; }
        [StringLength(100)]
        public string ContractId { get; set; }
        public Guid Token { get; set; }

        [Required]
        [StringLength(50)]
        public string DSLUserStatus { get; set; }
        public bool IsPrePaid { get; set; }
        public bool IsMigrated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
