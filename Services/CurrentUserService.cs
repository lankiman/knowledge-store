using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class CurrentUserService(
        UserManager<UserModel> userManager,
        IHttpContextAccessor httpContextAccessor)
        : BaseService(userManager, httpContextAccessor), ICurrentUserService
    {
        public async Task<UserModel?> GetCurrentUser()
        {
            var user = await userManager!.GetUserAsync(httpContextAccessor!.HttpContext!.User);
            return user;
        }

        public async Task<IList<string>?> GetCurrentUserRole()
        {
            var user = await GetCurrentUser();

            var userRoles = await userManager!.GetRolesAsync(user!);

            return userRoles;
        }
    }
}