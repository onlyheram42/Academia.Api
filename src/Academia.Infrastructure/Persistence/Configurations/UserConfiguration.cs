using Academia.Domain.Enums;
using Academia.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Role_Users",
                "Role IN ('Student', 'Admin', 'Teacher')");
        });

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.MiddleName)
            .HasMaxLength(100);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Username)
            .IsRequired();
        builder.Property(x => x.PasswordHash)
            .IsRequired();
        builder.Property(x => x.Role)
            .IsRequired()
            .HasConversion<string>()  // Store enum as string in database
            .HasDefaultValue(Role.Student);

        builder.HasIndex(x => x.Username)
            .IsUnique();
    }
}
