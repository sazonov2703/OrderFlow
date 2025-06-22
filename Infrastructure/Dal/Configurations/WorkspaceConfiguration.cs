using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.ToTable(nameof(Workspace));
        
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name)
            .HasColumnName(nameof(Workspace.Name))
            .IsRequired()
            .HasMaxLength(25);

        
        builder.HasMany(w => w.UserWorkspaces)
            .WithOne(uw => uw.Workspace)
            .HasForeignKey(uw => uw.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(w => w.Customers)
            .WithOne(c => c.Workspace)
            .HasForeignKey(c => c.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(w => w.Products)
            .WithOne(p => p.Workspace)
            .HasForeignKey(p => p.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(w => w.Orders)
            .WithOne(o => o.Workspace)
            .HasForeignKey(o => o.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}