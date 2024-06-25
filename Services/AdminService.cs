using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services
{
    public class AdminService(
        ELearningDbContext eLearningContext,
        UserManager<UserModel> userManager,
        IHttpContextAccessor httpContextAccessor)
        : BaseService(eLearningContext, userManager, httpContextAccessor), IAdminService
    {
        public async Task<AdminDto> GetAuthenticatedAdmin()
        {
            var user = await userManager!.GetUserAsync(HttpContext.User);

            return new AdminDto(user!);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await eLearningContext!.Users.ToListAsync();
            return users;
        }
    }
}