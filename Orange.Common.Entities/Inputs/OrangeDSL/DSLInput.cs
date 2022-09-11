using System;

namespace Orange.Common.Entities
{
    public class DSLInput : MobileInput
    {
        public string LandLineNumber { get; set; } //will be injected
        public Guid Token { get; set; } //will be injected
        public int DSLUserStatus { get; set; } //will be injected
        public bool IsPrePaid { get; set; } //will be injected
        public bool IsMigrated { get; set; } //will be injected
        public string UserName { get; set; } //will be injected

    }
}
