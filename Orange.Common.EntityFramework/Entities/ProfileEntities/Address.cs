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
    [Table("Address")]
    [DataContract]
    public partial class Address
    {

        [Key]
        [DataMember]
        [Column("Id")]
        public Int32 Id { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public int? CountryId { get; set; }

        [DataMember]
        public int? StateProvinceId { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string ZipPostalCode { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string FaxNumber { get; set; }

        [DataMember]
        public string CustomAttributes { get; set; }

        [Required]
        [DataMember]
        public DateTime CreatedOnUtc { get; set; }

        [ForeignKey("AddressID")]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
