using System;

namespace CreaDev.Framework.Core.Models
{
    public class AuditData
    {
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedByUserId { get; set; }
        public string ModifiedByUserId { get; set; }
    }
}