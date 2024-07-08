using e_learning.Enums;
using e_learning.Models;


namespace e_learning.DataTransfersObjects
{
    public class LessonDto(LessonModel lesson)
    {
        public string LessonId { get; set; } = lesson.LessonId;


        public string? LessonName { get; set; } = lesson.LessonName;


        public string? LessonDescription { get; set; } = lesson.LessonDescription;


        public LessonCategory LessonCategory { get; set; } = lesson.LessonCategory;

        public int LessonViews { get; set; } = lesson.LessonViews;

        public int LessonLikes { get; set; } = lesson.LessonLikes;


        public string? LessonVideoUrl { get; set; } = lesson.LessonVideoUrl;

        public InstructorDto LessonOwner { get; set; } = new(lesson.LessonOwner);
    }
}