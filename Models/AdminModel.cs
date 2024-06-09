namespace e_learning.Models
{
    public class AdminModel : UserModel
    {
        public bool IsAdmin { get; set; }

        public ICollection<LessonModel> Lessons { get; } = new List<LessonModel>();
    }
}