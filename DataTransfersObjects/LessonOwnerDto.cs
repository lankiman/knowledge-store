using e_learning.Models;
using System.Security.Policy;

namespace e_learning.DataTransfersObjects
{
    public class LessonOwnerDto(InstructorDto instructor)
    {
        public string? Firstname = instructor.Firstname;

        public string? Lastname = instructor.Lastname;

        public string? Id = instructor.Id;

        public IEnumerable<LessonModel> InstructorOwnedLessons = instructor.UserOwnedLessons;
    }
}