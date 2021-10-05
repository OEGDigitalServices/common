namespace Orange.Common.EntityFramework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChannelsData")]
    public partial class ChannelsData
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string ChannelName { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
