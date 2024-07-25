using System.Security.Claims;
using e_learning.Data;
using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
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

        // private async Task<IQueryable<UserModel>> FilterUsers(IQueryable<UserModel> users, string filters)
        // {
        //     switch (filters)
        //     {
        //         case "subscribed":
        //             users = users.Where(
        //                 user => userManager.GetClaimsAsync(user).Result.Any(c => c.Type == "Subscribed"));
        //             break;
        //         case "unsubscribed":
        //             users = users.Where(
        //                 user => userManager.GetClaimsAsync(user).Result.Any(c => c.Type != "Subscribed"));
        //             break;
        //     }
        //
        //
        //     return users;
        // }

        private async Task<IQueryable<UserModel>> FilterUsers(IQueryable<UserModel> users, string filters)
        {
            // Fetch the users into memory
            var userList = await users.ToListAsync();

            // Create a dictionary to hold user-claims associations
            var claimsDictionary = new Dictionary<UserModel, IList<Claim>>();

            // Populate the dictionary with user-claims pairs
            foreach (var user in userList)
            {
                claimsDictionary[user] = await userManager.GetClaimsAsync(user);
            }

            // Filter users based on the claims
            switch (filters.ToLower())
            {
                case "subscribed":
                    userList = userList
                        .Where(user => claimsDictionary[user].Any(c => c.Type == "Subscribed"))
                        .ToList();
                    break;
                case "unsubscribed":
                    userList = userList
                        .Where(user => !claimsDictionary[user].Any(c => c.Type == "Subscribed"))
                        .ToList();
                    break;
            }

            // Convert the filtered list back to IQueryable
            return userList.AsQueryable();
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

            Console.WriteLine($"{filters} from service");

            if (!string.IsNullOrEmpty(filters))
            {
                Console.WriteLine($"{filters} from service condtion");
                users = await FilterUsers(users, filters);
                Console.WriteLine($"{users.Count()} from condition method call");
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