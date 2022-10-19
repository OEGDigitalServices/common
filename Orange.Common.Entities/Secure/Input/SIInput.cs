using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
    public class SIInput
    {
        public string SourceId { get; set; }
        public Guid CorrelationId { get; set; }
        public string Msisdn { get; set; }
        public string Language { get; set; }
        public string Channel { get; set; }
        public string Method { get; set; }
        public string ModuleName { get; set; }
    }
}
