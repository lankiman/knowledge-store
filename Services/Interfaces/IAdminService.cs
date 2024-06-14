using e_learning.Models;

namespace e_learning.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<UserModel> GetAuthenticatedAdmin();
    }
}