using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class AdminModel : UserModel
    {
        [Required] public bool IsAdmin { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();

        public ICollection<LessonModel> Lessons { get; } = new List<LessonModel>();
    }
}