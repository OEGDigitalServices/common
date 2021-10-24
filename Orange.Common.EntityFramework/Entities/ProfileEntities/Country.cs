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
    [Table("Country")]
    [DataContract]
    public class Country
    {

        [Key]
        [Required]
        [DataMember]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [DataMember]
        public bool AllowsBilling { get; set; }

        [Required]
        [DataMember]
        public bool AllowsShipping { get; set; }

        [StringLength(2)]
        [DataMember]
        public string TwoLetterIsoCode { get; set; }

        [StringLength(3)]
        [DataMember]
        public string ThreeLetterIsoCode { get; set; }

        [Required]
        [DataMember]
        public int NumericIsoCode { get; set; }

        [Required]
        [DataMember]
        public bool SubjectToVat { get; set; }

        [Required]
        [DataMember]
        public bool Published { get; set; }

        [Required]
        [DataMember]
        public int DisplayOrder { get; set; }

        [Required]
        [DataMember]
        public bool LimitedToStores { get; set; }
    }
}
