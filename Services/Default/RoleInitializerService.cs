﻿using Microsoft.AspNetCore.Identity;

namespace e_learning.Services.Default
{
    public class RoleInitializerService(IServiceProvider serviceProvider)
    {
        public async Task InitializeRoles()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Instructor", "Admin", "Learner" };

                foreach (var roleName in roles)
                {
                    if (!roleManager.RoleExistsAsync(roleName).Result)
                    {
                        var role = new IdentityRole(roleName);
                        await roleManager.CreateAsync(role);
                    }
                }
            }
        }
    }
}