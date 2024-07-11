using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class LessonController(ILessonService lessonService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> PlayVideo(string filePath)
        {
            var result = lessonService.PlayVideo(filePath);


            return result;
        }
    }
}