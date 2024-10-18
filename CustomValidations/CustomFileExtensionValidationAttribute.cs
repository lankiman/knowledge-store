using System.ComponentModel.DataAnnotations;

namespace e_learning.CustomValidations
{
    public class CustomFileExtensionValidationAttribute(string[] allowedExtension) : ValidationAttribute
    {
        private readonly string[] _allowedExtension = allowedExtension;
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            //CreateLessonViewModel instanceCreateLessonViewModel =
            //    (CreateLessonViewModel)validationContext.ObjectInstance;


            if (value is IFormFile file)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (!_allowedExtension.Contains(fileExtension.ToLower()))
                {
                    return new ValidationResult($"Only {string.Join(", ", _allowedExtension)} are allowed ");

                }
            }
            return ValidationResult.Success;
        }
    }
}