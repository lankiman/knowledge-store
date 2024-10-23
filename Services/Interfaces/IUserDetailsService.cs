using e_learning.DataTransfersObjects;
using e_learning.Models;

namespace e_learning.Services.Interfaces
{
    public interface IUserDetailsService
    {
        public Task<string> GetUserId();
        public Task<UserModel?> GetUser();

        public Task<UserDto?> GetUserDetails();

        public Task<UserDto?> GetUserDetails(string userId);

        public Task<IList<string>?> GetUserRole();

        public Task<IList<string>?> GetUserRole(UserModel user);
    }
}