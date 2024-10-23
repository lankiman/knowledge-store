namespace e_learning.Views.Instructor.ViewModels
{
    public class StudioViewModel(string activeView)
    {
        public string ActiveView { get; set; } = activeView;
        public CreateLessonViewModel CreateLessonView = new CreateLessonViewModel();
    }
}
