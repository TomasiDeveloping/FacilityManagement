using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.User)
            .WithMany(u => u.Address)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(a => a.City).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(150);
        builder.Property(a => a.HouseNumber).IsRequired(false).HasMaxLength(10);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
        builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(8);
        builder.Property(a => a.Phone).IsRequired(false).HasMaxLength(100);
        builder.Property(a => a.Mobile).IsRequired(false).HasMaxLength(100);
    }
}