using Microsoft.AspNetCore.Mvc;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace e_learning.Controllers
{
    [Authorize(Roles = "Creator")]
    public class CreatorController(
        ILessonService lessonService,
        ICreatorService creatorService) : Controller
    {
        // GET:Creator
        public async Task<IActionResult> CreatorDashboard()
        {
            var user = await creatorService.GetAuthenticatedCreator();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            return View();
        }
    }
}