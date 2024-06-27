using e_learning.Models;

namespace e_learning.Services.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<UserModel?> GetCurrentUser();

        public Task<IList<string>?> GetCurrentUserRole();
    }
}