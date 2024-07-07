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
            if (user == null)
            {
                return null;
            }

            return user;
        }


        public async Task<string>? GetUserId()
        {
            var user = await GetUser();

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }


        public async Task<IList<string>?> GetUserRole()
        {
            var user = await GetUser();

            if (user == null)
            {
                return null;
            }

            var userRoles = await userManager!.GetRolesAsync(user);

            return userRoles;
        }

        public async Task<IList<string>?> GetUserRole(UserModel user)
        {
            var userRoles = await userManager!.GetRolesAsync(user!);

            return userRoles;
        }
    }
}