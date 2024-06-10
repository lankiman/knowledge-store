using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class AdminModel
    {
        [Key] [Required] public Guid Id { get; set; }
        [Required] [MaxLength(50)] public string Username { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string Password { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();

        public ICollection<LessonModel> Lessons { get; } = new List<LessonModel>();
    }
}