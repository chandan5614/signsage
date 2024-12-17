using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignSage.Core.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid RoleID { get; set; }
        public Role Role { get; set; }
    }
}