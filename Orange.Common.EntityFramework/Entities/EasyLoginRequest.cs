using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public class EasyLoginRequest
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string Dial { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(255)]
        public string UserAgent { get; set; }

        [StringLength(50)]
        public string Channel { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }
        [StringLength(50)]

        public string TempPassword { get; set; }
    }
}
