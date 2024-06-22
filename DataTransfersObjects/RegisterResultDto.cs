using e_learning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.DataTransfersObjects
{
    public class RegisterResultDto(IActionResult actionResult, IdentityResult identityResult)
    {
        public IActionResult ActionResult { get; set; } = actionResult;
        public IdentityResult Result { get; set; } = identityResult;
    }
}