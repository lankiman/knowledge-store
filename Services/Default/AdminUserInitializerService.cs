using e_learning.Models;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services.Default
{
    public class AdminUserInitializerService
    {
        private readonly UserManager<UserModel> _userManager;

        public AdminUserInitializerService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }
    }
}