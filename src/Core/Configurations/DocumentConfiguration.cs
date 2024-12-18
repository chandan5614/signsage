using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            // Table name
            builder.ToTable("Documents");

            // Primary Key
            builder.HasKey(d => d.Id);

            // Properties
            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Description)
                .HasMaxLength(1000);

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.Property(d => d.UpdatedAt)
                .IsRequired(false);

            // Relationships
            builder.HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Signatures)
                .WithOne(s => s.Document)
                .HasForeignKey(s => s.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(d => d.Title).IsUnique(false);
        }
    }
}
