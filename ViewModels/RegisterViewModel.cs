using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace e_learning.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Passoword is required")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}|:""<>?~`-=\[\]\\;',./]).{8,}$", ErrorMessage = "Password Too weak")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Field is required")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:\+?234|0)?[789][01]\d{8}$", ErrorMessage = "Invalid Phone number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string? Username { get; set; }
    }
}