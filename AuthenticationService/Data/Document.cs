using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Data
{
    public class Document
    {
        public Guid DocumentID { get; set; }
        public Guid TemplateID { get; set; }
        public Guid CustomerID { get; set; }
        public string Status { get; set; } // Enum: Draft, Sent, Signed
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}