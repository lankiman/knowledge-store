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

        private async Task<List<UserModel>> GetAllUsersModel()
        {
            var users = await eLearningContext!.Users.ToListAsync();
            return users;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await eLearningContext!.Users.ToListAsync();

            var user = await UserDetailsService.GetUser();

            var userList = users.Where(u => u.Id != user!.Id).ToList();

            var result = new List<UserDto>();

            foreach (var person in userList)
            {
                var personDto = new UserDto(person);
                result.Add(personDto);
            }

            return result;
        }

        public async Task<List<UserDto>> GetInstructors()
        {
            var users = await GetAllUsersModel();
            var instructors = new List<UserDto>();

            foreach (var user in users)
            {
                var userRoles = await UserDetailsService.GetUserRole(user);

                if (userRoles != null)
                {
                    var usersList = users.Where(c => userRoles.Contains("Creator")).ToList();

                    foreach (var person in usersList)
                    {
                        var personDto = new UserDto(person);
                        instructors.Add(personDto);
                    }
                }
            }

            return instructors;
        }
    }
}