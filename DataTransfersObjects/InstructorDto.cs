using e_learning.Models;

namespace e_learning.DataTransfersObjects
{
    public class InstructorDto(UserModel user)
    {
        public string? Id { get; set; } = user.Id;
        public string? UserName { get; set; } = user.UserName;

        public string? Firstname = user.FirstName;

        public string? Lastname = user.LastName;

        public string? Email { get; set; } = user.Email;

        public ICollection<LessonModel> UserOwnedLessons { get; set; } = user.UserOwnedLessons;
    }
}