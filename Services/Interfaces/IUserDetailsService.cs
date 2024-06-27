using e_learning.Models;

namespace e_learning.Services.Interfaces
{
    public interface IUserDetailsService
    {
        public Task<UserModel?> GetUser();

        public Task<IList<string>?> GetUserRole();

        public Task<IList<string>?> GetUserRole(UserModel user);
    }
}