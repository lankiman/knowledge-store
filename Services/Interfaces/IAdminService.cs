using e_learning.DataTransfersObjects;
using e_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;


namespace e_learning.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminDto> GetAuthenticatedAdmin();

        public Task<List<UserDto>> GetAllUsers();

        public Task<List<UserDto>> GetInstructors();

        public Task<IActionResult> AddInstructor(UserModel user);
    }
}