using e_learning.DataTransfersObjects;
using Microsoft.AspNetCore.Mvc;


namespace e_learning.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<LoginResultDto> LoginUser(string loginIdentifier, string password,
            bool rememberMe);

        public Task<IActionResult> LogoutUser();

        public Task<IActionResult> RegisterUser();
    }
}