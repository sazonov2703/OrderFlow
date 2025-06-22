using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasColumnName(nameof(User.Email))
            .HasMaxLength(25);

        builder.Property(u => u.Username)
            .HasColumnName(nameof(User.Username))
            .IsRequired()
            .HasMaxLength(25);
        
        builder.Property(u => u.HashedPassword)
            .HasColumnName(nameof(User.HashedPassword))
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasMany(u => u.UserWorkspaces)
            .WithOne(uw => uw.User)
            .HasForeignKey(uw => uw.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}