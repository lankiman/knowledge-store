using e_learning.Models;
using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

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

            return View(userLoginInfo);
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userRegistrationInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.RegisterUser(userRegistrationInfo);

                switch (result.ActionResult)
                {
                    case OkResult:

                        //return RedirectToAction("AdminDashboard", "Admin");
                        break;

                    case BadRequestResult:
                        foreach (var error in result.Result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        break;

                    case ConflictObjectResult { StatusCode: 409 } conflictObjectResult:
                        ModelState.AddModelError("", conflictObjectResult.Value!.ToString()!);
                        break;

                    case ObjectResult { StatusCode: 500 }:
                        ModelState.AddModelError("", "Server error occurred.");
                        break;
                }
            }

            return View(userRegistrationInfo);
        }
    }
}