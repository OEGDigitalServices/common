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
    [Table("ChannelsPrivileges")]
    public class ChannelsPrivilege
    {
        [Key]
        public Int32 ID { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        [StringLength(255)]
        public string Method { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
