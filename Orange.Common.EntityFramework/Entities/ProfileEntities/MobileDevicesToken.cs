using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public partial class MobileDevicesToken
    {
        [Key]
        public int Id { get; set; }

        [StringLength(12)]
        public string DialNumber { get; set; }

        public string DeviceToken { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsAndroid { get; set; }
    }
}
