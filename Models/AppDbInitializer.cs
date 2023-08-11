//using eCommerceWebApp.Data.Static;

using ExpenseTracker.Models.Static;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models;

public static class AppDbInitializer
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

        //Roles
        var roleManager =
            serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            _ = await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            _ = await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //Users
        var userManager =
            serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        const string adminUserEmail = "admin@etickets.com";

        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        if (adminUser == null)
        {
            ApplicationUser newAdminUser = new()
            {
                FullName = "Admin User",
                UserName = "admin-user",
                Email = adminUserEmail,
                EmailConfirmed = true
            };
            _ = await userManager.CreateAsync(newAdminUser, "Coding@1234?");
            _ = await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        }

        const string appUserEmail = "user@etickets.com";

        var appUser = await userManager.FindByEmailAsync(appUserEmail);
        if (appUser == null)
        {
            ApplicationUser newAppUser = new()
            {
                FullName = "Application User",
                UserName = "app-user",
                Email = appUserEmail,
                EmailConfirmed = true
            };
            _ = await userManager.CreateAsync(newAppUser, "Coding@1234?");
            _ = await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
        }
    }
}