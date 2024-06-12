using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Models
{
    public class UserModel : IdentityUser
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public string? MiddleName { get; set; }

        public ICollection<UserPaidLessonsModel> UserPaidLessons { get; set; } = new List<UserPaidLessonsModel>();

        public ICollection<LessonModel> UserOwnedLessons { get; set; } = new List<LessonModel>();
    }
}