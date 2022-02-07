namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CallBlockerContact
    {
        public int ID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string OriginalNumber { get; set; }

        [StringLength(50)]
        public string Dial { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string ListType { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }
    }
}
