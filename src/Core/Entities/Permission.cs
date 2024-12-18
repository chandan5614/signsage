using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignSage.Core.Entities
{
    public class Permission
    {
        public Guid PermissionID { get; set; }
        public Guid RoleID { get; set; }
        public string PermissionName { get; set; }
    }
}