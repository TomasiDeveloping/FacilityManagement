using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.DataSeeding;

public static class SeedData
{
    public static void SeedServiceAdministrator(ModelBuilder builder, IConfigurationSection userSection)
    {
        var serviceAdministratorEmail = userSection["Email"];
        var passwordHasher = new PasswordHasher<User>();
        var serviceAdministrator = new User
        {
            Id = new Guid("529BA03E-574A-4332-8775-A4E0D8F26ED4"),
            FirstName = "Service",
            LastName = "Administrator",
            Function = "Application Administrator",
            Email = serviceAdministratorEmail,
            UserName = serviceAdministratorEmail,
            NormalizedEmail = serviceAdministratorEmail!.ToUpper(),
            EmailConfirmed = true,
            NormalizedUserName = serviceAdministratorEmail.ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null!, userSection["Password"]!)
        };

        builder.Entity<User>().HasData(serviceAdministrator);
    }

    public static void SeedRoles(ModelBuilder builder)
    {
        var roles = new List<UserRole>
        {
            new()
            {
                Id = new Guid("4E4625F6-2973-4FA2-B820-A2F42B6D0037"),
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            }
        };
        builder.Entity<UserRole>().HasData(roles);
    }

    public static void SeedUserRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = new Guid("4E4625F6-2973-4FA2-B820-A2F42B6D0037"),
                UserId = new Guid("529BA03E-574A-4332-8775-A4E0D8F26ED4")
            }
        );
    }
}