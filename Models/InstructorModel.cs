using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace e_learning.Models
{
    public class InstructorModel : UserModel
    {
        public List<LessonModel>? InstructorLessons { get; set; }

        [Range(0, 5)] public decimal Rating { get; set; }
    }
}