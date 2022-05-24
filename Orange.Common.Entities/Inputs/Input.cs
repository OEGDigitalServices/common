using System;

namespace Orange.Common.Entities
{
    public class Input
    {
        public string Dial { get; set; }
        public bool IsEasyLogin { get; set; }
        public string Language { get; set; }
        public Guid UserId { get; set; }
        public Channel Channel { get; set; }
        public ModulesNames ModuleName { get; set; }
        public Guid RequestId { get; set; }
    }
}
