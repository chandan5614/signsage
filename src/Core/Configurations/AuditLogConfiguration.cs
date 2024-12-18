using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            // Table name
            builder.ToTable("AuditLogs");

            // Primary Key
            builder.HasKey(al => al.Id);

            // Properties
            builder.Property(al => al.Id)
                .ValueGeneratedOnAdd();

            builder.Property(al => al.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(al => al.Timestamp)
                .IsRequired();

            builder.Property(al => al.UserId)
                .IsRequired();

            builder.Property(al => al.EntityName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(al => al.EntityId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(al => al.Details)
                .HasMaxLength(2000);

            // Indexes
            builder.HasIndex(al => al.Timestamp).IsUnique(false);
        }
    }
}
