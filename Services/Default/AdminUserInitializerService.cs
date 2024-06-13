using e_learning.Models;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services.Default
{
    public class AdminUserInitializerService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        public async Task InitializeAdmin()
        {
            string email = configuration["Admin:DefaultAdminEmail"];
            string password = configuration["Admin:DefaultAdminPassword"];


            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new UserModel();

                    user.Email = email;
                    user.UserName = "lankiman";
                    user.FirstName = "lankiman";
                    user.LastName = "admin";
                    user.EmailConfirmed= true;

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description, error.Code);
                            return;
                        }
                    }
                }
            }
        }
    }
}