using e_learning.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace e_learning.CustomValidations
{
    public class ConfirmPasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            RegisterViewModel instanceRegisterViewModel = (RegisterViewModel)validationContext.ObjectInstance;

            if (instanceRegisterViewModel.Password != instanceRegisterViewModel.ConfirmPassword)
            {
                return new ValidationResult("Confirm Password and Password Fields do not match");
            }

            return ValidationResult.Success;
        }
    }
}