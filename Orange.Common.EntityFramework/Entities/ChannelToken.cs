using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.EntityFramework
{
    public class ChannelToken
    {
        public int ID { get; set; }

        public Guid? TokenValue { get; set; }

        [StringLength(100)]
        public string Channel { get; set; }

        [StringLength(50)]
        public string ErrorCode { get; set; }

        [StringLength(1000)]
        public string ErrorDescription { get; set; }

        [StringLength(50)]
        public string ConsumptionStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(500)]
        public string RequestedService { get; set; }

        [StringLength(255)]
        public string TokenHashedValue { get; set; }

        public string RequestBody { get; set; }

        public double? ResponseTimeForCreationInSeconds { get; set; }

        public double? ResponseTimeForConsumptionInSeconds { get; set; }

        [StringLength(500)]
        public string ServerIP { get; set; }  
    }
}
