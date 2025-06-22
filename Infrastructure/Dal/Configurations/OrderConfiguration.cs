using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Dal.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Description)
            .HasColumnName(nameof(Order.Description))
            .HasMaxLength(255);
        
        builder.Property(o => o.TotalAmount)
            .HasColumnName(nameof(Order.TotalAmount))
            .IsRequired();

        builder.Property(o => o.Status)
            .HasColumnName(nameof(Order.Status))
            .HasConversion(
                status => status.Name,
                name => Status.FromName(name))
            .IsRequired();
        
        
        builder.HasOne(o => o.Workspace)
            .WithMany(w => w.Orders)
            .HasForeignKey(o => o.WorkspaceId);
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(p => p.RecipentName)
                .HasColumnName(nameof(Order.ShippingAddress.RecipentName))
                .HasMaxLength(25);
            
            sa.Property(p => p.Country)
                .HasColumnName(nameof(Order.ShippingAddress.Country))
                .HasMaxLength(25);
            
            sa.Property(p => p.City)
                .HasColumnName(nameof(Order.ShippingAddress.City))
                .HasMaxLength(25);

            sa.Property(p => p.Street)
                .HasColumnName(nameof(Order.ShippingAddress.Street))
                .HasMaxLength(25);
            
            sa.Property(p => p.HouseNumber)
                .HasColumnName(nameof(Order.ShippingAddress.HouseNumber))
                .HasMaxLength(25);
            
            sa.Property(p => p.FlatNumber)
                .HasColumnName(nameof(Order.ShippingAddress.FlatNumber))
                .HasMaxLength(25);

            sa.Property(p => p.ZipCode)
                .HasColumnName(nameof(Order.ShippingAddress.ZipCode))
                .HasMaxLength(25);
        });
    }
}