using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace e_learning.Models
{
    public class InstructorModel
    {
        [Key] public string Id { get; set; }
        public List<LessonModel>? InstructorLessons { get; set; }

        [Range(0, 5)] public decimal Rating { get; set; }

        [ForeignKey("Id")] public UserModel User { get; set; }
    }
}