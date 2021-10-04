using Orange.Shared.Common.Enums;
using System;

namespace Orange.Common.Entities
{
    public class RequestModel
    {
        public string Dial { get; set; }
        public Channel Channel { get; set; }
        public ModulesNames ModuleName { get; set; }
        public Guid RequestID { get; set; }
        public string lang { get; set; }
    }
}
