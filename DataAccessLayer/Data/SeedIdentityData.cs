using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Data
{
    public static class SeedIdentityData
    {
        public static async Task InitializeAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            // ========================================
            // Create Roles
            // ========================================

            string[] roles =
            {
                "Admin",
                "User"
            };


            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role)
                    );
                }
            }


            // ========================================
            // Default Admin
            // ========================================

            string adminEmail =
                "admin@mawjoood.com";

            string adminPassword =
                "Admin@12345";


            var admin =
                await userManager.FindByEmailAsync(
                    adminEmail
                );


            // ========================================
            // Create Admin User
            // ========================================

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,

                    Email = adminEmail,

                    EmailConfirmed = true,

                    FullName = "System Admin"
                };


                var result =
                    await userManager.CreateAsync(
                        admin,
                        adminPassword
                    );


                if (!result.Succeeded)
                {
                    var errors =
                        string.Join(
                            ", ",
                            result.Errors.Select(e => e.Description)
                        );

                    throw new Exception(
                        "Failed to create admin: " +
                        errors
                    );
                }
            }


            // ========================================
            // Add Admin Role
            // ========================================

            if (!await userManager.IsInRoleAsync(
                    admin,
                    "Admin"))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    "Admin"
                );
            }
        }
    }
}