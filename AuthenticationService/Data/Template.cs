using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Data
{
    public class Template
    {
        public Guid TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateContent { get; set; }
        public int Version { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}