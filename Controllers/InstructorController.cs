using Microsoft.AspNetCore.Mvc;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace e_learning.Controllers
{
    [Authorize(Roles = "Creator")]
    public class InstructorController(
        ILessonService lessonService,
        IInstructorService instructorService) : Controller
    {
        // GET:Creator
        public async Task<IActionResult> InstructorDashboard()
        {
            var user = await instructorService.GetAuthenticatedInstructor();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            return View();
        }
    }
}