using System.Reflection;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class ApplicationDbContext : IdentityDbContext<User, UserRole, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Address> Address { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Maintenance> Maintainances { get; set; }
    public DbSet<Appointment> Appointments { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        DataSeeding.SeedData.SeedServiceAdministrator(builder, _configuration.GetSection("ServiceUser"));
        DataSeeding.SeedData.SeedRoles(builder);
        DataSeeding.SeedData.SeedUserRoles(builder);
    }
}