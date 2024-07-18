using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminDto> GetAuthenticatedAdmin();

        public Task<UserDto> GetUserDetails(string userId);

        public int GetUsersCount();

        public Task<AllUsersViewModel> GetAllUsers(int currentPage = 1, string? searchTerm = "", string? filters = "");

        public Task<List<InstructorDto>> GetInstructors();

        public Task<IActionResult> AddInstructor(UserModel? user);
    }
}