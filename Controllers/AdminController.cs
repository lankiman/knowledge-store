using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    [Route("splendid/")]
    [Authorize(Roles = "Admin")]
    public class AdminController(
        IAdminService adminService,
        IUserService userService,
        ILessonService lessonService,
        UserManager<UserModel> userManager) : Controller
    {
        public async Task<IActionResult> AdminDashboard()
        {
            var user = await adminService.GetAuthenticatedAdmin();
            return View(user);
        }
    }
}