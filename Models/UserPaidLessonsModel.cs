using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;

namespace e_learning.Models
{
    public class UserPaidLessonsModel
    {
        // [Key, Column(Order = 0)] public string UserId { get; set; }
        //
        // [Key, Column(Order = 1)] public string LessonId { get; set; }
        //
        // [ForeignKey("UserId")] public UserModel User { get; set; }
        //
        // [ForeignKey("LessonId")] public LessonModel Lesson { get; set; }
    }
}