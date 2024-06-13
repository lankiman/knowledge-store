using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        public IActionResult Login(LoginViewModel userLoginInfo)
        {
            accountService.LoginUser(userLoginInfo.LoginIdentifier, userLoginInfo.Password,
                userLoginInfo.RememberMe);

            //var user = await userManager.GetUserAsync(User);
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}