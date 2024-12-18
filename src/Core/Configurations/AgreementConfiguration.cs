using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
    {
        public void Configure(EntityTypeBuilder<Agreement> builder)
        {
            // Table name
            builder.ToTable("Agreements");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .IsRequired(false);

            builder.Property(a => a.IsActive)
                .IsRequired();

            // Relationships
            builder.HasMany(a => a.Documents)
                .WithOne(d => d.Agreement)
                .HasForeignKey(d => d.AgreementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(a => a.Title).IsUnique(false);
        }
    }
}
