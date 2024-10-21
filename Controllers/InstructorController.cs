using e_learning.Services.Interfaces;
using e_learning.ViewModels;
using e_learning.Views.Instructor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace e_learning.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController(
        ILessonService lessonService,
        IInstructorService instructorService) : Controller
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userDetails = await instructorService.GetUserDetails();
                context.HttpContext.Items["InstructorDetails"] = new LayoutsViewModel(userDetails);
            }

            await next();
        }

        // GET:Creator
        public async Task<IActionResult> InstructorDashboard()
        {
            var instructor = await instructorService.GetInstructor();
            return View(instructor);
        }

        public async Task<IActionResult> InstructorLessons()
        {
            var lessons = await instructorService.GetInstructorLessons();

            if (lessons == null)
            {
                return View(null);
            }

            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Studio()
        {

            var activeForm = HttpContext.Session.GetString("activeView") ?? "lesson";

            var viewModel = new StudioViewModel(activeForm, new UploadVideoViewModel());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadLessonVideo(IFormFile file)
        {
            var result = await instructorService.UploadLessonToTempStorage(file);

            switch (result)
            {
                case OkObjectResult okResult:

                    return Ok(okResult.Value);

                case ObjectResult objectResult:
                    return StatusCode(objectResult.StatusCode.Value, objectResult.Value);

                default:

                    return StatusCode(500, new { Message = "An unexpected error occurred." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CompleteLessonDetails(UploadVideoViewModel lessonData, string tempLessonId)
        {
            if (ModelState.IsValid)
            {
                var result = await instructorService.CompleteLessonDetails(lessonData, tempLessonId);
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