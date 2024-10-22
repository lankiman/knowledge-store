using e_learning.CustomValidations;
using e_learning.Enums;
using System.ComponentModel.DataAnnotations;

namespace e_learning.Views.Instructor.ViewModels
{
    public class CreateLessonViewModel
    {
        [Required(ErrorMessage = "Please Enter Name of Lesson")]
        public string? LessonName { get; set; }

        [Required(ErrorMessage = "Please Enter a Lesson Description")]
        public string? LessonDescription { get; set; }

        [Required(ErrorMessage = "Please choose a category")]
        public LessonCategory? LessonCategory { get; set; }


        [Required(ErrorMessage = "Please choose Acess type")]
        public AcessType? LessonAcessType { get; set; }

        [CustomFileExtensionValidation([".png", ".jpg", ".gif"])]
        [Required(ErrorMessage = "Please Choose a Thumbnail")]
        public IFormFile LessonThumbnail { get; set; }
    }
}
