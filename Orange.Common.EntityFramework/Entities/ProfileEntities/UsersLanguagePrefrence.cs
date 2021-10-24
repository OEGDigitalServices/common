using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public partial class UsersLanguagePrefrence
    {
        [Key]
        public int ID { get; set; }

        [StringLength(256)]
        public string Dial { get; set; }

        [StringLength(256)]
        public string Language { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
