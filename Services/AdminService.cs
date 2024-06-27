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
        IUserDetailsService userDetailsService)
        : BaseService(eLearningContext, userManager, userDetailsService), IAdminService
    {
        public async Task<AdminDto> GetAuthenticatedAdmin()
        {
            var user = await UserDetailsService.GetUser();
            return new AdminDto(user!);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await eLearningContext!.Users.ToListAsync();

            var user = await UserDetailsService.GetUser();

            var userList = users.Where(u => u.Id != user!.Id).ToList();

            return userList;
        }

        public async Task<List<UserModel>> GetCreators()
        {
            var users = await GetAllUsers();
            var creators = new List<UserModel>();

            foreach (var user in users)
            {
                var userRoles = await UserDetailsService.GetUserRole(user);

                if (userRoles != null)
                {
                    creators = users.Where(c => userRoles.Contains("Creator")).ToList();
                }
            }

            return creators;
        }
    }
}