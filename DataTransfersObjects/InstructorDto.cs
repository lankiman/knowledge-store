using e_learning.Models;


namespace e_learning.DataTransfersObjects
{
    public class InstructorDto(InstructorModel instructor)
    {
        public string? Id { get; set; } = instructor.Id;
        public IEnumerable<LessonModel>? InstructorLessons { get; set; } = instructor.InstructorLessons;

        public UserDto? InstructorDetails { get; set; } = new(instructor.User);
    }
}