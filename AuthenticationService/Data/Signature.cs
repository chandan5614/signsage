using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Data
{
    public class Signature
    {
        public Guid SignatureID { get; set; }
        public Guid DocumentID { get; set; }
        public string SignerName { get; set; }
        public string SignerEmail { get; set; }
        public string Status { get; set; } // Enum: Pending, Signed, Declined
        public DateTime? SignedAt { get; set; }
    }
}