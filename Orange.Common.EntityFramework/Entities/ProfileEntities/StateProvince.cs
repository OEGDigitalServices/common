using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    [Serializable]
    [Table("StateProvince")]
    [DataContract]
    public class StateProvince
    {
        [Key]
        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [StringLength(100)]
        [DataMember]
        public string Abbreviation { get; set; }

        [DataMember]
        public bool Published { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }
    }
}
