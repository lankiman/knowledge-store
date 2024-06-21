using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class AccountController(IAccountService accountService, UserManager<UserModel> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userLoginInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.LoginUser(userLoginInfo.LoginIdentifier, userLoginInfo.Password,
                    userLoginInfo.RememberMe);

                switch (result.ActionResult)
                {
                    case OkResult:
                        if (result.Roles != null && result.Roles.Contains("Admin"))
                        {
                            return RedirectToAction("AdminDashboard", "Admin");
                        }

                        break;

                    case UnauthorizedResult:
                        ModelState.AddModelError("", "Invalid Username/Email or Password");
                        break;

                    case ObjectResult { StatusCode: 500 }:
                        ModelState.AddModelError("", "Server error occurred.");
                        break;
                }
            }

            userLoginInfo.LoginIdentifier = "";
            return View(userLoginInfo);
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}