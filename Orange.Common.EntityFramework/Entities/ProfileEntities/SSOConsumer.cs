namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SSOConsumer
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string ConsumerName { get; set; }

        [StringLength(255)]
        public string ConsumerIP { get; set; }

        [StringLength(255)]
        public string Method { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

    }
}
