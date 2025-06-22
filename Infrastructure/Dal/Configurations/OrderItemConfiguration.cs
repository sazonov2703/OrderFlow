using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(nameof(OrderItem));
        
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ProductName)
            .HasColumnName(nameof(OrderItem.ProductName))
            .HasMaxLength(50);
        
        builder.Property(oi => oi.ProductUnitPrice)
            .HasColumnName(nameof(OrderItem.ProductUnitPrice))
            .IsRequired();
        
        builder.Property(oi => oi.Quantity)
            .HasColumnName(nameof(OrderItem.Quantity))
            .IsRequired();
        
        builder.Property(oi => oi.TotalPrice)
            .HasColumnName(nameof(OrderItem.TotalPrice))
            .IsRequired();


        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);
        
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
    }
}