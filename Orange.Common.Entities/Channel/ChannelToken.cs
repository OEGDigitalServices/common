using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
   public class ChannelToken
    {
        public int ID { get; set; }
        public Nullable<System.Guid> TokenValue { get; set; }
        public string Channel { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string ConsumptionStatus { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string RequestedService { get; set; }
        public string TokenHashedValue { get; set; }
        public string RequestBody { get; set; }
        public Nullable<double> ResponseTimeForCreationInSeconds { get; set; }
        public Nullable<double> ResponseTimeForConsumptionInSeconds { get; set; }
        public string ServerIP { get; set; }
        public Nullable<bool> IsRevoked { get; set; }
        public string UserName { get; set; }
    }
}
