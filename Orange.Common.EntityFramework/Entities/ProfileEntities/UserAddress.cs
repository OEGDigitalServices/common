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
    [Table("UserAddresses")]
    public class UserAddress
    {
        [Key, Column(Order = 0)]
        public Guid UserID { get; set; }

        [Key, Column(Order = 1)]
        public int AddressID { get; set; }

        public virtual Address Address { get; set; }
    }
}
