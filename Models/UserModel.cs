using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class UserModel
    {
        [Key][Required] public Guid Id { get; set; } = new Guid();
        [Required] public string UserName { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public string? MiddleName { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string PhoneNumber { get; set; }
    }
}