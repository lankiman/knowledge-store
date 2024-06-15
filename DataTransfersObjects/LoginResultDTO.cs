using e_learning.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.DataTransfersObjects
{
    public class LoginResultDto(IActionResult actionResult, UserModel? user, IList<string>? roles)
    {
        public IActionResult ActionResult { get; set; } = actionResult;
        public UserModel? User { get; set; } = user;
        public IList<string>? Roles { get; set; } = roles;
    }
}