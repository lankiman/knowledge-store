using e_learning.Models;

namespace e_learning.DataTransfersObjects
{
    public class AdminDto(UserModel user)
    {
        public string? Id { get; set; } = user.Id;
        public string? UserName { get; set; } = user.UserName;

        public List<UserModel> Users = new List<UserModel>();
    }
}