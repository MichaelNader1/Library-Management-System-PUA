using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Models;

public static class SeedUsers
{
    public static async Task SeedBasicUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AdminUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var users = new List<(string Username, string Password, string Role, int LibraryID)>
        {
            ("Michael", "P@ssw0rd", "User", 1),
            ("Omar", "P@ssw0rd", "Admin", 1)
        };

        foreach (var userData in users)
        {
            if (await userManager.FindByNameAsync(userData.Username) == null)
            {
                var user = new AdminUser
                {
                    UserName = userData.Username,
                    LibraryID = userData.LibraryID 
                };

                var result = await userManager.CreateAsync(user, userData.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userData.Role);
                }
            }
        }
    }
}
