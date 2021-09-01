using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
   public class ServicesFailedRequest
    {
        public string Dial { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Channel { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
