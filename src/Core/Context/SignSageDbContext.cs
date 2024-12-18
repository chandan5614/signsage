using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignSage.Core.Entities;

namespace SignSage.Core.Context
{
    public class SignSageDbContext : DbContext
    {
        public SignSageDbContext(DbContextOptions<SignSageDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Renewal> Renewals { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}