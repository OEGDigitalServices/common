namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServicesUsersIPsLog
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Dial { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        [StringLength(255)]
        public string UserIPValue { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(50)]
        public string Method { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
