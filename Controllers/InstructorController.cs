using Microsoft.AspNetCore.Mvc;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using e_learning.ViewModels;


namespace e_learning.Controllers
{
    [Authorize(Roles = "Instructor")]
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

        // [HttpPost]
        // public async Task<IActionResult> CreateLesson(CreateLessonViewModel lessonData)
        // {
        //     return View();
        // }
    }
}