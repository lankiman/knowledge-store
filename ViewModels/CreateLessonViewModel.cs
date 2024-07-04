﻿using e_learning.CustomValidations;
using e_learning.Enums;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace e_learning.ViewModels
{
    public class CreateLessonViewModel
    {
        [Required(ErrorMessage = "Please Enter Name of Lesson")]
        public string? LessonName { get; set; }

        [Required(ErrorMessage = "Please Enter a Lesson Description")]
        public string? LessonDescription { get; set; }

        [Required(ErrorMessage = "Please choose a category")]
        public LessonCategory LessonCategory { get; set; }

        [DataType(DataType.Date)] public string? CreatedAt { get; set; }

        [Required(ErrorMessage = "Please Choose a Video File")]
        // [CustomFileExtensionValidation]
        public IFormFile? LessonVideo { get; set; }
    }
}