using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Data
{
    public class AuditTrail
    {
        public Guid AuditID { get; set; }
        public Guid DocumentID { get; set; }
        public string Action { get; set; } // Enum: Created, Updated, Sent, Signed, Declined
        public Guid PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}