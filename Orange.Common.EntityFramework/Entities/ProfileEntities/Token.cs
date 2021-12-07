using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework.Entities
{
    public class Token
    {
        public int ID { get; set; }

        public Guid? TokenValue { get; set; }

        public Guid? UserID { get; set; }

        [StringLength(50)]
        public string Dial { get; set; }

        [StringLength(255)]
        public string Consumer { get; set; }

        public string ConsumptionStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(255)]
        public string GenerateTokenServerIP { get; set; }

        [StringLength(255)]
        public string BurnTokenServerIP { get; set; }
        
        [StringLength(255)]
        public string UserIP { get; set; }
        
        [StringLength(255)]
        public string UserAgent { get; set; }
        public bool IsEasyLogin { get; set; }
    }
}
