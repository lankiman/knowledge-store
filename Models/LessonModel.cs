using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_learning.Enums;
using Microsoft.AspNetCore.Authorization;


namespace e_learning.Models
{
    public class LessonModel
    {
        [Key] [Required] public string LessonId { get; set; } = Guid.NewGuid().ToString();


        [Required] public string LessonName { get; set; }

        [Required] public string LessonDescription { get; set; }

        [Required] public LessonCategory LessonCategory { get; set; }

        public int LessonViews { get; set; }

        public int LessonLikes { get; set; }

        [Required] public string LessonVideoUrl { get; set; }

        [ForeignKey("LessonOwner")] [Required] public string LessonOwnerId { get; set; }

        [Required] public UserModel LessonOwner { get; set; }

        public ICollection<UserPaidLessonsModel> UserPaidLessons { get; set; }
    }
}