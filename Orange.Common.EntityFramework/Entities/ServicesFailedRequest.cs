namespace Orange.Common.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServicesFailedRequest
    {
        public int ID { get; set; }

        [StringLength(15)]
        public string Dial { get; set; }

        [StringLength(50)]
        public string ControllerName { get; set; }

        [StringLength(500)]
        public string ActionName { get; set; }

        [StringLength(50)]
        public string Channel { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ErrorCode { get; set; }

        [StringLength(300)]
        public string ErrorDescription { get; set; }
    }
}
