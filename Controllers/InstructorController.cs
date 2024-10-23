using e_learning.Services.Interfaces;
using e_learning.Views.Instructor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace e_learning.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController(
        IUserDetailsService userDetailsService,
        ILessonService lessonService,
        IInstructorService instructorService) : BaseController(userDetailsService)
    {
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

        public IActionResult Studio()
        {

            var activeForm = HttpContext.Session.GetString("activeStudioView") ?? "lesson";

            var viewModel = new StudioViewModel(activeForm);

            return View(viewModel);
        }

        //Ajax Actions
        [HttpPost]
        public async Task<IActionResult> SetActiveStudioView(string activeStudioView)
        {
            HttpContext.Session.SetString("activeStudioView", activeStudioView);
            return Ok(activeStudioView);
        }

        [HttpPost]
        public async Task<IActionResult> UploadLessonVideo(IFormFile file)
        {
            var result = await instructorService.UploadLessonToTempStorage(file);

            return result switch
            {
                OkObjectResult okResult => Ok(okResult.Value),
                ObjectResult objectResult => StatusCode(objectResult.StatusCode.Value, objectResult.Value),
                _ => StatusCode(500, new { Message = "An unexpected error occurred." }),
            };
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLessonDetails(Views.Instructor.ViewModels.CreateLessonViewModel lessonData, string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await instructorService.CompleteLessonDetails(lessonData, Id);
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

        //    [HttpPost]
        //    [RequestSizeLimit(268435456)]
        //    [RequestFormLimits(MultipartBodyLengthLimit = 268435456)]
        //    public async Task<IActionResult> CreateLesson(CreateLessonViewModel lessonData)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = await instructorService.CreateLesson(lessonData);

        //            switch (result)
        //            {
        //                case OkObjectResult:
        //                    break;

        //                case ObjectResult { StatusCode: 500 }:
        //                    ModelState.AddModelError("", "An Error Occured");
        //                    break;
        //            }
        //        }

        //        return View(lessonData);
        //    }
        //}
    }
}