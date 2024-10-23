using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    public class LearnerController : Controller
    {
        [Authorize(Roles = "Learner")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}