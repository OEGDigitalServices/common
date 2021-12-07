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
    [Table("AddressAttribute")]
    public class AddressAttribute
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(400)]
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public int AttributeControlTypeId { get; set; }

        public int DisplayOrder { get; set; }
    }
}
