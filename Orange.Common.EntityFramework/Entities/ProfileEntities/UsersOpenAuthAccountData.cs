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
    [Table("UsersOpenAuthAccountData")]
    public class UsersOpenAuthAccountData
    {
        [Key, ForeignKey("UsersOpenAuthAccount")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UsersOpenAuthAccountID { get; set; }

        //[StringLength(128)]
        public string Name { get; set; }

        //[StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        //[StringLength(255)]
        public string Link { get; set; }

        [StringLength(255)]
        public string Picture { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public virtual UsersOpenAuthAccount UsersOpenAuthAccount { get; set; }
    }
}
