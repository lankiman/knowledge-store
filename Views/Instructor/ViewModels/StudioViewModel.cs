namespace e_learning.Views.Instructor.ViewModels
{
    public class StudioViewModel(string activeView, UploadVideoViewModel lessonModel)
    {
        public string ActiveView { get; set; } = activeView;
        public UploadVideoViewModel LessonModel { get; set; } = lessonModel;
        public static StudioViewModel ForLessonUpload(UploadVideoViewModel model) => new("lesson", model);
    }


}
