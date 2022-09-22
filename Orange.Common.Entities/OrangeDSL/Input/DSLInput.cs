using System;

namespace Orange.Common.Entities
{
    public class DSLInput : MobileInput
    {
        public string LandLineNumber { get; set; } 
        public Guid DSLToken { get; set; } 
        public string DSLUserStatus { get; set; }
        public bool IsPrePaid { get; set; } 
        public bool IsMigrated { get; set; } 
        public string UserName { get; set; } 
        public string UCID { get; set; } 
        public string CustomerId { get; set; }
        public string ContractId { get; set; }
    }
}
