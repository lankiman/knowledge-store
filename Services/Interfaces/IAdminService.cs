using e_learning.DataTransfersObjects;


namespace e_learning.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminUserDto> GetAuthenticatedAdmin();
    }
}