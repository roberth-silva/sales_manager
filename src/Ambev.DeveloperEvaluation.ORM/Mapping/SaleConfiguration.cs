using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(s => s.SaleNumber)
            .HasColumnName("sale_number")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.SaleDate)
            .HasColumnName("sale_date")
            .IsRequired();

        builder.Property(s => s.Customer)
            .HasColumnName("customer")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnName("total_amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(s => s.Branch)
            .HasColumnName("branch")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .HasColumnName("is_cancelled")
            .IsRequired();

        builder.HasMany(s => s.Items)
            .WithOne(i => i.Sale)
            .HasForeignKey(i => i.SaleId)            
            .OnDelete(DeleteBehavior.Cascade);
    }
}
