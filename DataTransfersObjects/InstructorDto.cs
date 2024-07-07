using e_learning.Models;
using System.Collections;

namespace e_learning.DataTransfersObjects
{
    public class InstructorDto(InstructorModel instructor)
    {
        public string? Id { get; set; } = instructor.Id;

        public IEnumerable<LessonModel>? InstructorLessons { get; set; } = instructor.InstructorLessons;
    }
}