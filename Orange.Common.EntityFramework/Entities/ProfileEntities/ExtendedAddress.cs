using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public partial class Address
    {
        [NotMapped]
        public string DisplayName { get; set; }
    }
}
