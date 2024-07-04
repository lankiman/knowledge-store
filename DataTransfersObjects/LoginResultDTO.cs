using e_learning.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.DataTransfersObjects
{
    public class LoginResultDto(IActionResult actionResult, UserDto? user, IList<string>? roles)
    {
        public IActionResult ActionResult { get; set; } = actionResult;
        public UserDto? User { get; set; } = user;
        public IList<string>? Roles { get; set; } = roles;
    }
}