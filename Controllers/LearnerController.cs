using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class LearnerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}