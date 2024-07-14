using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

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

        public async Task<UserDto> GetUserDetails(string userId)
        {
            var userDetails = await userDetailsService.GetUserDetails(userId);

            if (userDetails == null)
            {
                return null!;
            }

            return userDetails;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await eLearningContext!.Users.ToListAsync();

            var currentUser = await UserDetailsService.GetUser();

            var instructorsIds = await eLearningContext.Instructors.Select(i => i.Id).ToListAsync();

            var tempUsersList = new List<UserModel>();

            foreach (var user in users)
            {
                foreach (var id in instructorsIds)
                {
                    if (user.Id != id)
                    {
                        tempUsersList.Add(user);
                    }
                }
            }

            var usersList = tempUsersList.Where(u => u.Id != currentUser!.Id).ToList();


            var result = new List<UserDto>();

            foreach (var person in usersList)
            {
                var personDto = new UserDto(person);
                result.Add(personDto);
            }

            return result;
        }

        public async Task<List<InstructorDto>> GetInstructors()
        {
            var currentUser = await UserDetailsService.GetUser();
            var instructors = await eLearningContext.Instructors.Where(i => i.User.Id != currentUser.Id).ToListAsync();
            var instructorsList = new List<InstructorDto>();

            foreach (var instructor in instructors)
            {
                var instructorDto = new InstructorDto(instructor);
                instructorsList.Add(instructorDto);
            }


            return instructorsList;
        }

        public async Task<IActionResult> AddInstructor(UserModel user)
        {
            try
            {
                var result = await userManager!.AddToRoleAsync(user, "Instructor");

                if (result.Succeeded)
                {
                    var instructor = new InstructorModel
                    {
                        Id = user.Id
                    };
                     eLearningContext.Instructors.Add(instructor);
                    
                    var changes=eLearningContext.SaveChanges();

                    if (changes > 1)
                    {
                        return new OkResult();
                    }
                    else
                    {
                        await userManager.RemoveFromRoleAsync(user, "Instructor");
                        eLearningContext.Instructors.Remove(instructor);
                        return new BadRequestResult();
                    }
                }

                return new OkResult();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.InnerException.Message);
               return new ObjectResult(new { Message = $"An error occurred: {ex.Message}" }) { StatusCode = 500 };
            }
        }
    }
}