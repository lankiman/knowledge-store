using Microsoft.AspNetCore.Identity;

namespace e_learning.Services.Default
{
    public class RoleInitializerService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleInitializerService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async void InitializeRoles()
        {
            var roles = new[] { "Admin", "SuperUser", "Learner" };

            foreach (var roleName in roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole(roleName);
                    var result = _roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            // Dispose of the RoleManager after initialization
            _roleManager.Dispose();
        }
    }
}