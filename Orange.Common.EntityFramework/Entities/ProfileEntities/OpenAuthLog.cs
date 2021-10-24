using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public class OpenAuthLog
    {
        public int ID { get; set; }

        public string Provider { get; set; }

        public string UserIDAtProvider { get; set; }

        public string Dial { get; set; }

        public Guid? UserID { get; set; }

        public System.DateTime? CreatedDate { get; set; }

        public System.DateTime? ModifiedDate { get; set; }

        public string Method { get; set; }

        public string Status { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }

        [StringLength(255)]
        public string UserAgent { get; set; }
    }
}
