using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services;

public class AccountService(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
    : IAccountService
{
    public async Task<IActionResult> LoginUser(string loginIdentifier, string password, bool rememberMe)
    {
        var result = await signInManager.PasswordSignInAsync(loginIdentifier, password, rememberMe, false);

        return new OkResult();
    }

    public async Task<IActionResult> LogoutUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> RegisterUser()
    {
        throw new NotImplementedException();
    }
}