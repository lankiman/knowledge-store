using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services
{
    public class AdminService(
        ELearningDbContext eLearningContext,
        UserManager<UserModel> userManager,
        ICurrentUserService currentUserService)
        : BaseService(eLearningContext, userManager, currentUserService), IAdminService
    {
        public async Task<AdminDto> GetAuthenticatedAdmin()
        {
            var user = await currentUserService.GetCurrentUser();
            return new AdminDto(user!);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await eLearningContext!.Users.ToListAsync();

            var user = await currentUserService.GetCurrentUser();

            var userList = users.Where(u => u.Id != user!.Id).ToList();

            return userList;
        }
    }
}