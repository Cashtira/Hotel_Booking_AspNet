using Microsoft.AspNetCore.Identity;
using MVCmodel.Models;

namespace MVCmodel.Data
{
    public class SeedRolesAndUsers
    {
        public static void SeedRolesAndUsersMethod(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                // Define roles
                var roleNames = new[] { "Admin", "User", "Manager" };

                // Seed roles
                foreach (var roleName in roleNames)
                {
                    var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                    if (!roleExist)
                    {
                        var role = new IdentityRole(roleName);
                        var result = roleManager.CreateAsync(role).Result;
                    }
                }

                // Create default Admin user
                var adminUser = userManager.FindByEmailAsync("admin@example.com").Result;
                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        FullName = " Admin User"
                    };

                    var createAdminResult = userManager.CreateAsync(adminUser, "Admin@123").Result;
                    if (createAdminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                // Create default Manager user (if needed)
                var managerUser = userManager.FindByEmailAsync("manager@example.com").Result;
                if (managerUser == null)
                {
                    managerUser = new User
                    {
                        UserName = "manager@example.com",
                        Email = "manager@example.com",
                        FullName= "Manager User"
                    };

                    var createManagerResult = userManager.CreateAsync(managerUser, "Manager@123").Result;
                    if (createManagerResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(managerUser, "Manager").Wait();
                    }
                }

                // Create default User user (if needed)
                var regularUser = userManager.FindByEmailAsync("user@example.com").Result;
                if (regularUser == null)
                {
                    regularUser = new User
                    {
                        UserName = "user@example.com",
                        Email = "user@example.com",
                        FullName = "Regular User"
                    };

                    var createUserResult = userManager.CreateAsync(regularUser, "User@123").Result;
                    if (createUserResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(regularUser, "User").Wait();
                    }
                }
            }
        }
    }
}
