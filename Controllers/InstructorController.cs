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
            var instructor = await instructorService.GetInstructor();


            return View(instructor);
        }

        public async Task<IActionResult> InstructorLessons()
        {
            var lessons = await instructorService.GetInstructorLessons();

            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        [RequestSizeLimit(268435456)]
        [RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        public async Task<IActionResult> CreateLesson(CreateLessonViewModel lessonData)
        {
            if (ModelState.IsValid)
            {
                var result = await instructorService.CreateLesson(lessonData);

                switch (result)
                {
                    case OkObjectResult:
                        break;

                    case ObjectResult { StatusCode: 500 }:
                        ModelState.AddModelError("", "An Error Occured");
                        break;
                }
            }

            return View(lessonData);
        }
    }
}