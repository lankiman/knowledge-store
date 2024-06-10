using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class UserModel
    {
        [Key] [Required] public Guid Id { get; set; }
        [Required] [MaxLength(50)] public string Username { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public string? MiddleName { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string PhoneNumber { get; set; }

        public ICollection<LessonModel> UserPaidLessons { get; set; } = new List<LessonModel>();
    }
}