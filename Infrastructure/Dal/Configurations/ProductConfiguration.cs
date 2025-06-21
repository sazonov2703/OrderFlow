using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasColumnName(nameof(Product.Name))
            .IsRequired()
            .HasMaxLength(25);
        
        builder.Property(p => p.Description)
            .HasColumnName(nameof(Product.Description))
            .HasMaxLength(255);
        
        builder.Property(p => p.ImageUrl)
            .HasColumnName(nameof(Product.ImageUrl))
            .HasMaxLength(255);
        
        builder.Property(p => p.UnitPrice)
            .HasColumnName(nameof(Product.UnitPrice))
            .IsRequired();
        
        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId);
        
        builder.HasOne(p => p.Workspace)
            .WithMany(w => w.Products)
            .HasForeignKey(p => p.WorkspaceId);
    }
}