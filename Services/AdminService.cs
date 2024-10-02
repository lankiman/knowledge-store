using System.Security.Claims;
using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Rules;


namespace e_learning.Services
{
    public class AdminService(
        ELearningDbContext eLearningContext,
        UserManager<UserModel> userManager,
        IUserDetailsService userDetailsService)
        : BaseService(eLearningContext, userManager, userDetailsService), IAdminService
    {
        private async Task<List<UserModel>> GetAllUsersModel()
        {
            var users = await eLearningContext!.Users.ToListAsync();
            return users;
        }

        private async Task<IQueryable<UserModel>> GetUsers()
        {
            var currentUser = await UserDetailsService.GetUser();

            var instructorsIds = await eLearningContext.Instructors.Select(i => i.Id).ToListAsync();

            var users = eLearningContext!.Users.Where(user =>
                !instructorsIds.Contains(user.Id) && user.Id != currentUser.Id);

            return users;
        }


        private async Task<IQueryable<UserModel>> FilterUsers(IQueryable<UserModel> users, string filters)
        {
            switch (filters)
            {
                case "subscribed":
                    users = users
                        .Where(user => user.Claims.Any(c => c.ClaimType.Contains("Subscribed")));
                    break;
                case "unsubscribed":
                    users = users
                        .Where(user => user.Claims.Any(c => !c.ClaimType.Contains("Subscribed")));
                    break;
            }

            return users;
        }


        public async Task<AdminDto> GetAuthenticatedAdmin()
        {
            var user = await UserDetailsService.GetUser();
            return new AdminDto(user!);
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

        public int GetUsersCount()
        {
            var result = GetUsers().Result.ToList();

            return result.Count;
        }

        public async Task<AllUsersViewModel> GetAllUsers(int currentPage = 1, string? searchTerm = "",
            string? filters = "")
        {
            var users = await GetUsers();


            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm.ToLower();

            int? pageSize = 10;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u =>
                    u.Firstname.ToLower().Contains(searchTerm) || u.Lastname.ToLower().Contains(searchTerm));
            }


            if (!string.IsNullOrEmpty(filters))
            {
                users = await FilterUsers(users, filters);
            }

            var result = new AllUsersViewModel
            {
                Users = users.Skip((int)((currentPage - 1) * pageSize)).Take((int)pageSize)
                    .Select(user => new UserDto(user)).ToList(),

                CurrentPage = currentPage,
                TotalPages = (int)(users.Count() / pageSize),
                SearchTerm = searchTerm,
                Filters = filters
            };

            Console.WriteLine($"{result.Users.Count} from service");

            return result;
        }

        public async Task<List<InstructorDto>> GetInstructors()
        {
            var currentUser = await UserDetailsService.GetUser();
            var instructors = await eLearningContext.Instructors.Where(i => i.User.Id != currentUser.Id)
                .Select(i => new InstructorDto(i)).ToListAsync();

            return instructors;
        }

        public async Task<IActionResult> AddInstructor(UserModel? user)
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

                    var changes = eLearningContext.SaveChanges();

                    if (changes > 1)
                    {
                        return new OkResult();
                    }

                    await userManager.RemoveFromRoleAsync(user, "Instructor");
                    eLearningContext.Instructors.Remove(instructor);
                    return new BadRequestResult();
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return new ObjectResult(new { Message = $"An error occurred: {ex.Message}" }) { StatusCode = 500 };
            }
        }
    }
}