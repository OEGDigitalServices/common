using System;

namespace Orange.Common.Entities
{
    public class ValidateDSLBasicAuthenticationTokenOutput
    {
        public bool IsValid { get; set; }
        public string Dial { get; set; }
        public Guid? Token { get; set; }
        public string DSLUserStatus { get; set; }
        public bool IsPrePaid { get; set; }
        public bool IsMigrated { get; set; }
        public string LandLineNumber { get; set; }
        public string UserName { get; set; }
        public string UCID { get; set; }
        public string CustomerId { get; set; }
        public string ContractId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}