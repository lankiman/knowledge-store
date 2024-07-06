using e_learning.Models;
using System.Collections;

namespace e_learning.DataTransfersObjects
{
    public class InstructorDto(InstructorModel instructor)
    {
        public string? Id { get; set; } = instructor.Id;
        public string? UserName { get; set; } = instructor.UserName;

        public string? Firstname { get; set; } = instructor.Firstname;

        public string? Lastname { get; set; } = instructor.Lastname;

        public string? MiddleName { get; set; } = instructor.MiddleName;

        public string? Email { get; set; } = instructor.Email;

        public IEnumerable<LessonModel>? InstructorLessons { get; set; } = instructor.InstructorLessons;
    }
}