using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Controllers
{
    [Authorize(Roles = "Learner")]
    public class LearnerController(IUserDetailsService userDetailsService) : BaseController(userDetailsService)
    {
        [Authorize(Roles = "Learner")]
        [Authorize(Roles = "Instructor")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}