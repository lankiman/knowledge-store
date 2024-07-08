using System.ComponentModel.DataAnnotations;
using e_learning.ViewModels;

namespace e_learning.CustomValidations
{
    public class CustomFileExtensionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            CreateLessonViewModel instanceCreateLessonViewModel =
                (CreateLessonViewModel)validationContext.ObjectInstance;

            if (!instanceCreateLessonViewModel.LessonVideo.Name.Contains(".mp4"))
            {
                return new ValidationResult("Only Mp4 Files are allowed");
            }

            return ValidationResult.Success;
        }
    }
}