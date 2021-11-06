namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProfileRequestsLog
    {
        public int ID { get; set; }

        public Guid? UserID { get; set; }

        [StringLength(20)]
        public string Dial { get; set; }

        [StringLength(255)]
        public string Method { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }

        [StringLength(255)]
        public string UserAgent { get; set; }
    }
}
