using e_learning.DataTransfersObjects;
using e_learning.Models;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    [Route("splendid/[action]")]
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
            var usersCount = await adminService.GetAllUsers();
            var creatorsCount = await adminService.GetInstructors();

            ViewData["UsersCount"] = usersCount.Count;
            ViewData["CreatorsCount"] = creatorsCount.Count;

            return View(user);
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await adminService.GetAllUsers();

            return View(users);
        }

        public async Task<IActionResult> UserDetails(UserDto user)
        {
            var users = await adminService.GetAllUsers();

            return View(users);
        }

        public async Task<IActionResult> Instructors()
        {
            var instructors = await adminService.GetInstructors();

            return View(instructors);
        }

        [HttpGet]
        public async Task<IActionResult> AddInstructor()
        {
            var instructors = await adminService.GetInstructors();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddInstructor(UserModel user)
        {
            return View();
        }
    }
}