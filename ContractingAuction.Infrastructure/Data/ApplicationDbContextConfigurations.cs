using ContractingAuction.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContractingAuction.Infrastructure.Data;

public class ApplicationDbContextConfigurations
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
    }

    public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        bool adminRoleExists = await roleManager.RoleExistsAsync(UserRoles.Admin.ToString());
        bool userRoleExists = await roleManager.RoleExistsAsync(UserRoles.User.ToString());
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
        }

        if (!userRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
        }

        IdentityUser adminUser = new()
        {
            Email = "admin@admin.com",
            UserName = "admin"
        };
        if (userManager.Users.All(u => u.Id != adminUser.Id))
        {
            IdentityUser? user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user is null)
            {
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin.ToString());
            }
        }

        IdentityUser simpleUser = new()
        {
            Email = "user@user.com",
            UserName = "user"
        };
        if (userManager.Users.All(u => u.Id != simpleUser.Id))
        {
            IdentityUser? user = await userManager.FindByEmailAsync(simpleUser.Email);
            if (user is null)
            {
                await userManager.CreateAsync(simpleUser, "User@123");
                await userManager.AddToRoleAsync(simpleUser, UserRoles.User.ToString());
            }
        }
    }
}