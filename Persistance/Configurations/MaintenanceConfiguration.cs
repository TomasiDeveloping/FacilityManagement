using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Interval).IsRequired();
        builder.Property(m => m.Name).IsRequired().HasMaxLength(250);
        builder.Property(m => m.LastExecution).IsRequired(false);
    }
}