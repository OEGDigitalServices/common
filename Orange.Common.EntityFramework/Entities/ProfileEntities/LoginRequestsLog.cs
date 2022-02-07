namespace Orange.Common.EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoginRequestsLog
    {
        public int ID { get; set; }

        [StringLength(128)]
        public string LoginProviderName { get; set; }
        [StringLength(128)]
        public string UserIDAtProvider { get; set; }
        [StringLength(20)]
        public string Dial { get; set; }
        public Guid? UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        [StringLength(255)]
        public string ServerIP { get; set; }
        [StringLength(255)]
        public string UserAgent { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Channel { get; set; }
        [StringLength(255)]
        public string UserIP { get; set; }
        public bool? IsAndroid { get; set; }
        [StringLength(255)]
        public string AppVersion { get; set; }
    }
}
