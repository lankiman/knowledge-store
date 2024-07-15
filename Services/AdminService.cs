﻿using e_learning.Data;
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

        private async Task<List<UserDto>> GetUsers()
        {
            var currentUser = await UserDetailsService.GetUser();

            var instructorsIds = await eLearningContext.Instructors.Select(i => i.Id).ToListAsync();

            var users = await eLearningContext!.Users.Where(user =>
                    !instructorsIds.Contains(user.Id) && user.Id != currentUser.Id).Select(user => new UserDto(user))
                .ToListAsync();

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

        public int GetUsersCount()
        {
            var count = GetUsers().Result.Count;

            return count;
        }

        public async Task<AllUsersViewModel> GetAllUsers(string? term = "")
        {
            var users = await GetUsers();

            var result = new AllUsersViewModel
            {
                Users = users
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