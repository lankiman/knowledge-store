using e_learning.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.DataTransfersObjects
{
    public class LoginResultDTO(IActionResult actionResult, UserModel? user, IList<string>? roles)
    {
        public IActionResult actionResult { get; set; } = actionResult;
        public UserModel? User { get; set; } = user;
        public IList<string>? Roles { get; set; } = roles;
    }
}