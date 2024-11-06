namespace e_learning.Views.Instructor.ViewModels
{
    public enum ActiveViewType
    {
        lesson,
        series
    }
    public class StudioViewModel(ActiveViewType activeView)
    {
        public ActiveViewType ActiveView { get; set; } = activeView;
        public CreateLessonViewModel CreateLessonView = new CreateLessonViewModel();
    }
}
