namespace Orange.Common.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChannelsPrivilege
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Channel { get; set; }

        [StringLength(255)]
        public string Method { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
