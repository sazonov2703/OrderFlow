using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasColumnName(nameof(Customer.FirstName))
            .HasMaxLength(25);
        
        builder.Property(c => c.LastName)
            .HasColumnName(nameof(Customer.LastName))
            .HasMaxLength(25);
        
        builder.Property(c => c.Patronymic)
            .HasColumnName(nameof(Customer.Patronymic))
            .HasMaxLength(25);
        
        builder.Property(c => c.Email)
            .HasColumnName(nameof(Customer.Email))
            .HasMaxLength(100);
        
        
        builder.OwnsMany(c => c.PhoneNumbers, b =>
        {
            b.ToTable("CustomerPhoneNumbers");
            b.WithOwner().HasForeignKey("CustomerId");

            b.Property(p => p).HasColumnName("PhoneNumber");
            b.HasKey("CustomerId", "PhoneNumber");
        });

        builder.OwnsMany(c => c.Links, b =>
        {
            b.ToTable("CustomerLinks");
            b.WithOwner().HasForeignKey("CustomerId");

            b.Property(l => l).HasColumnName("Link");
            b.HasKey("CustomerId", "Link");
        });
        
        
        builder.HasOne(c => c.Workspace)
            .WithMany(w => w.Customers)
            .HasForeignKey(c => c.WorkspaceId);

        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);
    }
}