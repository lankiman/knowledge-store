﻿using System.ComponentModel.DataAnnotations;

namespace e_learning.Models
{
    public class LessonModel
    {
        [Key] [Required] public Guid LessonId { get; set; } = new Guid();

        [Required] public string LessonName { get; set; }

        [Required] public string LessonDescription { get; set; }

        [Required] public string LessonCategory { get; set; }


        public int? LessonViews { get; set; }


        public int? LessonLikes { get; set; }

        [Required] public byte[] LessonVideo { get; set; }


        [Required] public string LessonOwnerId { get; set; }

        [Required] public AdminModel LessonOwner { get; set; } = null!;
    }
}