using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignSage.Core.Entities
{
    public class Renewal
    {
        public Guid RenewalID { get; set; }
        public Guid DocumentID { get; set; }
        public DateTime RenewalDate { get; set; }
        public string Status { get; set; } // Enum: Pending, Approved, Completed
        public Guid? ApprovedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}