using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            // Table name
            builder.ToTable("Licenses");

            // Primary Key
            builder.HasKey(l => l.Id);

            // Properties
            builder.Property(l => l.Id)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.LicenseKey)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(l => l.IssuedDate)
                .IsRequired();

            builder.Property(l => l.ExpirationDate)
                .IsRequired();

            builder.Property(l => l.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.UserId)
                .IsRequired();

            // Relationships
            builder.HasOne(l => l.User)
                .WithMany(u => u.Licenses)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(l => l.LicenseKey).IsUnique();
        }
    }
}
