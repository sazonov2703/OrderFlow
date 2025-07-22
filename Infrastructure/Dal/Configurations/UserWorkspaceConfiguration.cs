using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class UserWorkspaceConfiguration : IEntityTypeConfiguration<UserWorkspace>
{
    public void Configure(EntityTypeBuilder<UserWorkspace> builder)
    {
        builder.ToTable(nameof(UserWorkspace));
        
        builder.HasKey(uw => new { uw.UserId, uw.WorkspaceId });
        
        builder.Property(uw => uw.Role)
            .HasColumnName(nameof(UserWorkspace.Role))
            .IsRequired();
        
        builder.HasOne(uw => uw.User)
            .WithMany(u => u.UserWorkspaces)
            .HasForeignKey(uw => uw.UserId);

        builder.HasOne(uw => uw.Workspace)
            .WithMany(w => w.UserWorkspaces)
            .HasForeignKey(uw => uw.WorkspaceId);
    }
}