using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignSage.Core.Models
{
    public class AuditTrail
    {
        [Key]
        public Guid AuditID { get; set; }
        public Guid DocumentID { get; set; }
        public string Action { get; set; } // Enum: Created, Updated, Sent, Signed, Declined
        public Guid PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}