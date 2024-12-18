using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Name
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.UserId);

            // Properties
            builder.Property(u => u.UserId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired(false)  // Set to false, confirming LastName is optional
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.UpdatedDate)
                .IsRequired(false); // UpdatedDate can be null initially

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Relationships
            builder.HasMany(u => u.Documents)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");
        }
    }
}
