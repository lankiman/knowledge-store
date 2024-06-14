using e_learning.Models;

namespace e_learning.DataTransfersObjects
{
    public class UserDto(UserModel user)
    {
        public string? Id { get; set; } = user.Id;
        public string? UserName { get; set; } = user.UserName;

        public string? Email { get; set; } = user.Email;

        public string? Firstname { get; set; } = user.FirstName;

        public string Lastname { get; set; } = user.LastName;


        public ICollection<LessonModel> UserOwnedLessons { get; set; } = user.UserOwnedLessons;
    }
}