using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class LessonController(ILessonService lessonService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> PlayVideo(string videoId)
        {
            var result = lessonService.PlayVideo(videoId);


            return result;
        }
    }
}