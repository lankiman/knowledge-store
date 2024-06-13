using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class AccountController(IAccountService accountService, UserManager<UserModel> userManager) : Controller
    {
        public IActionResult Login(LoginViewModel userLoginInfo)
        {
            var result = accountService.LoginUser(userLoginInfo.LoginIdentifier, userLoginInfo.Password,
                userLoginInfo.RememberMe);

            var roles = result.Result.Roles;

            var user = result.Result.User;


            if (roles != null && roles.Contains("Admin"))
            {
            }


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