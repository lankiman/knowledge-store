using e_learning.Data;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Services
{
    public class AdminService(
        ELearningDbContext eLearningContext,
        UserManager<UserModel> userManager,
        IHttpContextAccessor httpContextAccessor)
        : BaseService(eLearningContext, userManager, httpContextAccessor), IAdminService
    {
        public async Task<UserModel> GetAuthenticatedAdmin()
        {
            var user = await userManager!.GetUserAsync(HttpContext.User);

            return user!;
        }
    }
}