using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class UserDetailsService(
        UserManager<UserModel> userManager,
        IHttpContextAccessor httpContextAccessor)
        : BaseService(userManager, httpContextAccessor), IUserDetailsService
    {
        public async Task<UserModel?> GetUser()
        {
            var user = await userManager!.GetUserAsync(httpContextAccessor!.HttpContext!.User);
            return user;
        }


        public async Task<IList<string>?> GetUserRole()
        {
            var user = await GetUser();

            var userRoles = await userManager!.GetRolesAsync(user!);

            return userRoles;
        }

        public async Task<IList<string>?> GetUserRole(UserModel user)
        {
            var userRoles = await userManager!.GetRolesAsync(user!);

            return userRoles;
        }
    }
}