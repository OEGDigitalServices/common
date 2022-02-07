namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserDial
    {
        public int ID { get; set; }

        public Guid? UserID { get; set; }

        [StringLength(50)]
        public string Dial { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsMobinil { get; set; }

        public bool? DialStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }

        public bool? IsGuest { get; set; }
    }
}
