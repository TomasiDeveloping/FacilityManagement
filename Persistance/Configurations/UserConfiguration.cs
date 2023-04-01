using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Function).IsRequired().HasMaxLength(250);
        builder.Property(u => u.PersonalNumber).IsRequired(false);
    }
}