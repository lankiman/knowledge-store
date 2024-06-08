using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class UserModel
    {
        [Required] public int Id { get; set; }

        [Required] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public string? MiddleName { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string PhoneNumber { get; set; }
    }
}