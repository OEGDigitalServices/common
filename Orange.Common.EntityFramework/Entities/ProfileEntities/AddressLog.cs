using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public class AddressLog
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public int? CountryId { get; set; }

        public int? StateProvinceId { get; set; }

        public string City { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string ZipPostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string CustomAttributes { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorDescription { get; set; }

        public Guid? UserID { get; set; }

        [StringLength(50)]
        public string Method { get; set; }
        
        [StringLength(255)]
        public string ServerIP { get; set; }

        [StringLength(255)]
        public string UserAgent { get; set; }

        [StringLength(255)]
        public string Channel { get; set; }

        [StringLength(255)]
        public string UserIP { get; set; }

    }
}
