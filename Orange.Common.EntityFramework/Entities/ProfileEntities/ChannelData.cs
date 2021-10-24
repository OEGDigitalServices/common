using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    [Serializable]
    [Table("ChannelsData")]
    public class ChannelData
    {
        [Key]
        public Int32 ID { get; set; }

        [StringLength(255)]
        public string ChannelName { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
