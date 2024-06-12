using System.ComponentModel.DataAnnotations;

namespace e_learning.ViewModels
{
    public class LoginViewModel
    {
        [Required] public string LoginIdentifier { get; set; }

        [Required] public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}