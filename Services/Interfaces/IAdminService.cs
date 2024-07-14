using e_learning.DataTransfersObjects;
using e_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;


namespace e_learning.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminDto> GetAuthenticatedAdmin();

        public Task<UserDto> GetUserDetails(string userId);

        public Task<List<UserDto>> GetAllUsers();

        public Task<List<InstructorDto>> GetInstructors();

        public Task<IActionResult> AddInstructor(UserModel user);
    }
}