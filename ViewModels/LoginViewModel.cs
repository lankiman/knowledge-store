using System.ComponentModel.DataAnnotations;

namespace e_learning.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter your username or email address")]
        public string? LoginIdentifier { get; set; }

        [Required(ErrorMessage = "Please Enter your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}