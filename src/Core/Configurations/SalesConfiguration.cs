using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Configurations
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            // Table name
            builder.ToTable("Sales");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.Product)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Indexes
            builder.HasIndex(s => s.SaleDate);
        }
    }
}
